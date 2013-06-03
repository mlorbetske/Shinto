using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.EntityModel
{
    public interface IEntityCache
    {
        IDictionary<TId, TEntity> GetCacheFor<TEntity, TId>() where TEntity : IEntity<TId>;
        bool Contains<TEntity, TId>(TId tgt) where TEntity : IEntity<TId>;
        void Add<TEntity, TId>(TEntity tgt) where TEntity : IEntity<TId>;
        void Remove<TEntity, TId>(TId tgt) where TEntity : IEntity<TId>;
        void Remove<TEntity, TId>(IEnumerable<TId> keys) where TEntity : IEntity<TId>;
    }

}
