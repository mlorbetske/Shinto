// http://creativecommons.org/licenses/by/3.0/
// Please consider giving me a link or shout out if you use this code 
// http://www.damonpayne.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shinto.EntityModel;

namespace Shinto.RepositoryModel
{
    /// <summary>
    /// Typed read-only operations for retrieving IEntities with a known Id type
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IRepository<TEntity, TId> : IRepository where TEntity : IEntity<TId>
    {
        /// <summary>
        /// Load & return all
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Load 1 by Id, or default(TEntity) if none found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(TId id);

        /// <summary>
        /// Return all TEntity matching the predicate May require an aggressive load.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

    }
}
