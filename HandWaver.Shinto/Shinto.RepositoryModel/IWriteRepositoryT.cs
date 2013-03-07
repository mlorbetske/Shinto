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
    /// Mutate operations for IEntity&lt;TId&gt;
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IWriteRepository<TEntity, TId> : IRepository where TEntity : IEntity<TId>
    {
        /// <summary>
        /// Create a new TEntity with an auto assigned id, or default(TId)
        /// </summary>
        /// <returns></returns>
        TEntity Create();

        /// <summary>
        /// Create a new TEntity with a given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Create(TId id);

        /// <summary>
        /// Persist these targets
        /// </summary>
        /// <param name="targets"></param>
        void Save(IEnumerable<TEntity> targets);

        /// <summary>
        /// Persist this target
        /// </summary>
        /// <param name="target"></param>
        void Save(TEntity target);

        /// <summary>
        /// Remove these targets from persisted store
        /// </summary>
        /// <param name="targets"></param>
        void Delete(IEnumerable<TEntity> targets);

        /// <summary>
        /// Remove a single target from persisted store
        /// </summary>
        /// <param name="targets"></param>
        void Delete(TEntity targets);
    }
}
