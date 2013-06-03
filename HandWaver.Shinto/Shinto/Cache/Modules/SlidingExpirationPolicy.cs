using System;

namespace Shinto.Cache.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public class SlidingExpirationPolicy : TimeBasedExpirationPolicy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="target"></param>
        /// <param name="accessDelta">A timespan for which this item can be un-accessed before being removed</param>
        public SlidingExpirationPolicy(object key, object target, TimeSpan accessDelta) : base(key,target)
        {
            _accessDelta = accessDelta;
        }

        TimeSpan _accessDelta;
        DateTime _lastAccessed;

        public override void ItemAccessed(object key, object item)
        {
            _lastAccessed = DateTime.Now;
        }

        protected override bool ShouldRemove(DateTime date)
        {
            TimeSpan currentDelta = date - _lastAccessed;
            bool shouldRemove = currentDelta > _accessDelta;
            if (shouldRemove)
            {
                string message = string.Format("Removing {0} due to SlidingExpirationPolicy of {1}", _key, _accessDelta);
                TimeBasedExpirationPolicy.Log.Debug(message);
            }
            return shouldRemove;
        }
    }
}
