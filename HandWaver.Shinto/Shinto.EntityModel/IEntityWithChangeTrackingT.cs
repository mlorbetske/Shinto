using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.EntityModel
{
    /// <summary>
    /// An IEntity that promises to register its changes with the supplied Tracker
    /// </summary>
    public interface IEntityWithChangeTracking : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        IEntityChangeTracker Tracker { get; set; }
    }
}
