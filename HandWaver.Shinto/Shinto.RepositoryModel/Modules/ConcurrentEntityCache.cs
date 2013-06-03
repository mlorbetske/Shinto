using Shinto.Cache;
using Shinto.Cache.Modules;
using Shinto.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Shinto.RepositoryModel.Modules
{
    [Export(typeof(IEntityCache))]
    public class ConcurrentEntityCache : IEntityCache
    {
        /// <summary>
        /// Flush unused entities if not accessed
        /// </summary>
        public static readonly TimeSpan EntityAccessTimeout = TimeSpan.FromMinutes(5.0);

        public ConcurrentEntityCache()
        {
            _rawCache = new CacheProvider();
        }

        ICacheProvider _rawCache;

        string GetEntityCacheKey<TEntity, TId>()
        {
            //TODO: something better than typeof?
            return string.Format("<{0},{1}>Cache", typeof(TEntity), typeof(TId));
        }

        public IDictionary<TId, TEntity> GetCacheFor<TEntity, TId>() where TEntity : IEntity<TId>
        {
            string key = GetEntityCacheKey<TEntity,TId>();
            if(!_rawCache.Contains(key))
            {
                var newCache = ConcurrentDictionaryProvider.Create<TEntity,TId>();
                _rawCache.Put(key, newCache, new SlidingExpirationPolicy(key, newCache, EntityAccessTimeout ) );
            }
            return _rawCache.Get<IDictionary<TId, TEntity>>(key);
        }

        public bool Contains<TEntity, TId>(TId tgt) where TEntity : IEntity<TId>
        {
            return GetCacheFor<TEntity, TId>().ContainsKey(tgt);
        }

        public void Add<TEntity, TId>(TEntity tgt) where TEntity : IEntity<TId>
        {
            GetCacheFor<TEntity, TId>()[tgt.Id] = tgt;
        }

        public void Remove<TEntity, TId>(TId tgt) where TEntity : IEntity<TId>
        {
            GetCacheFor<TEntity, TId>().Remove(tgt);
        }

        public void Remove<TEntity, TId>(IEnumerable<TId> ids) where TEntity : IEntity<TId>
        {
            var cache = GetCacheFor<TEntity, TId>();
            foreach (var id in ids)
            {
                cache.Remove(id);
            }            
        }
    }
}
