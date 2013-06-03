using Shinto.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Shinto.RepositoryModel.Modules
{
    public class Repository<TEntity, TId> where TEntity: IEntity<TId>
    {
        [Import]
        public IEntityCache Cache { get; set; }

        protected virtual void PostSave(IEnumerable<TEntity> values)
        {
        }

        /// <summary>
        /// Subset of keys not in cache for TEntity, TId
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected virtual IEnumerable<TId> GetCacheMisses(IEnumerable<TId> keys)
        {

            var cache = Cache.GetCacheFor<TEntity, TId>();
            var cacheMisses = keys.Where(e => !cache.ContainsKey(e));
            return cacheMisses;


        }

        protected virtual void MergeIntoCache(IEnumerable<TEntity> entities)
        {

            var cache = Cache.GetCacheFor<TEntity, TId>();
            foreach (var e in entities)
            {
                cache[e.Id] = e;
            }
        }

        /// <summary>
        /// Assumes all values have already been merged into cache
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected virtual IEnumerable<TEntity> GetFromCache(IEnumerable<TId> keys)
        {

            var results = new List<TEntity>(keys.Count());
            var cache = Cache.GetCacheFor<TEntity, TId>();
            foreach (var k in keys)
            {

                TEntity tgt = default(TEntity);
                if (cache.TryGetValue(k, out tgt))
                {
                    results.Add(tgt);
                }
            }

            return results;


        }

        /// <summary>
        /// Assumes all values have already been merged into cache
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected virtual IEnumerable<TEntity> GetFromCache(IEnumerable<TId> keys, out IEnumerable<TId> cacheMisses)
        {

            var results = new List<TEntity>(keys.Count());
            var cache = Cache.GetCacheFor<TEntity, TId>();
            var misses = new List<TId>();
            foreach (var k in keys)
            {

                TEntity tgt = default(TEntity);
                if (cache.TryGetValue(k, out tgt))
                {
                    results.Add(tgt);
                }

                else
                {
                    misses.Add(k);
                }
            }
            cacheMisses = misses;

            return results;


        }


    }
}
