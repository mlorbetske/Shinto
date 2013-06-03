using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.EntityModel
{
    public interface IEntityCache
    {
        IDictionary<TKey, TEntity> GetCacheFor<TEntity, TKey>() where TEntity : IEntity<TKey>;
        bool Contains<TEntity, TKey>(TKey tgt) where TEntity : IEntity<TKey>;
        void Add<TEntity, TKey>(TEntity tgt) where TEntity : IEntity<TKey>;
        void Remove<TEntity, TKey>(TKey tgt) where TEntity : IEntity<TKey>;
        void Remove<TEntity, TKey>(IEnumerable<TKey> keys) where TEntity : IEntity<TKey>;
    }

}
