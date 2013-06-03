using System;
using System.Threading;
using System.Collections.Generic;

namespace Shinto.Cache.Modules
{
    public abstract class TimeBasedExpirationPolicy : ICacheExpirationPolicy
    {
        static int ExpirationCheckSleep = 100000;

        static TimeBasedExpirationPolicy()
        {
            Initializer.SatisfyImports<TimeBasedExpirationPolicy>();
            StartCheckExpiredThread();

            _classSyncRoot = new object();
        }

        public static IConcurrentDictionary<object, TimeBasedExpirationPolicy> ActiveTimePolicies { get; set; }

        protected static object _classSyncRoot;

        public static IThreadFactory ThreadFactory { get; set; }

        static void StartCheckExpiredThread()
        {
            ThreadFactory.CreateAndStart("TimeBasedExpirationPolicyThread", true, CheckExpiration);
        }

        static void CheckExpiration()
        {
            while (true)
            {
                ThreadFactory.Sleep(Thread.CurrentThread, ExpirationCheckSleep);
                List<TimeBasedExpirationPolicy> keysToRemove = new List<TimeBasedExpirationPolicy>();
                foreach (TimeBasedExpirationPolicy policy in ActiveTimePolicies.Values)
                {
                    if (policy.ShouldRemove(DateTime.Now))
                    {
                        keysToRemove.Add(policy);
                    }
                }
                foreach (TimeBasedExpirationPolicy policy in keysToRemove)
                {
                    policy._cacheProvider.Remove(policy._key);
                    TimeBasedExpirationPolicy tgt;
                    ActiveTimePolicies.TryRemove(policy._key, out tgt);
                }
            }
        }

        public static ILogger Log { get; set; }

        public TimeBasedExpirationPolicy(object key, object target)
        {
            _key = key;
            _target = target;
        }

        protected object _key;
        protected object _target;

        ICacheProvider _cacheProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="date">The date/time against which to judge expiration</param>
        /// <returns></returns>
        protected abstract bool ShouldRemove(DateTime date);

        public void ItemAdded(object key, object item)
        {
            ActiveTimePolicies.TryAdd(key, this);
        }

        public virtual void ItemAccessed(object key, object item)
        {
            //do nothing by default
        }

        public void SetProvider(ICacheProvider provider)
        {
            _cacheProvider = provider;
        }

        public void ItemRemoved(object key, object item)
        {
            if (item == _target)
            {
                TimeBasedExpirationPolicy tgt;
                ActiveTimePolicies.TryRemove(key, out tgt);                
            }
        }
    }
}
