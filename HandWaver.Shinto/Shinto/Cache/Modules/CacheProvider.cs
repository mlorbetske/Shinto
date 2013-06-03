using System;
using System.Threading;
using System.Collections.Generic;

namespace Shinto.Cache.Modules
{
    /// <summary>
    /// Default cache provider
    /// </summary>    
    public class CacheProvider : ICacheProvider
    {
        public CacheProvider()
        {            
            _defaultPolicy = new NullExpirationPolicy();
            Cache = ConcurrentDictionaryProvider.Create<object, ItemPolicyPair>();
        }

        
        public IConcurrentDictionary<object, ItemPolicyPair> Cache { get; set; }

        NullExpirationPolicy _defaultPolicy;

        public void Put(object key, object item)
        {
            Put(key, item, _defaultPolicy);
        }

        public void Put(object key, object item, ICacheExpirationPolicy expirationPolicy)
        {
            expirationPolicy.SetProvider(this);
            Cache[key] = new ItemPolicyPair(item, expirationPolicy);
            expirationPolicy.ItemAdded(key, item);
        }

        public void Remove(object key)
        {
            ItemPolicyPair pair;
            if (Cache.TryRemove(key, out pair))
            {
                pair.ExpirationPolicy.ItemRemoved(key, pair.Item);
            }
        }

        public bool Contains(object key)
        {
            return Cache.ContainsKey(key);
        }

        public T Get<T>(object key)
        {
            T value = default(T);

            ItemPolicyPair pair = null;
            if (Cache.TryGetValue(key, out pair))
            {
                pair.ExpirationPolicy.ItemAccessed(key, pair.Item);
            }

            return value;
        }


        public void Clear()
        {
            Cache.Clear();
        }
    }
}
