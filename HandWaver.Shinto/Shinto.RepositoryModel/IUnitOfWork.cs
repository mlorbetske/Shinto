using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shinto.EntityModel;

namespace Shinto.RepositoryModel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        IEntityChangeTracker Tracker { get; }

        /// <summary>
        /// 
        /// </summary>
        void CommitAllChanges();
    }
}
