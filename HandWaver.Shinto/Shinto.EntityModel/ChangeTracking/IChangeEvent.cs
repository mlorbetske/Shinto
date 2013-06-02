using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.EntityModel.ChangeTracking
{
    /// <summary>
    /// Represents a single change to an Entity
    /// </summary>
    public interface IChangeEvent
    {
        /// <summary>
        /// The Entity that was changed. For entity relationships, the parent. For a Collection event, the Entity that owns the collection.
        /// </summary>
        IEntity AffectedEntity { get; set; }

        /// <summary>
        /// The Id of the AffectedEntity
        /// </summary>
        object Id { get; set; }

        /// <summary>
        /// The type of AffectedEntity
        /// </summary>
        string Type { get; set; }
    }
}
