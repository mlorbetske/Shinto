using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.EntityModel.ChangeTracking
{
    /// <summary>
    /// A non-relationship property has changed
    /// </summary>
    public interface IPropertyChangeEvent : IChangeEvent
    {
        /// <summary>
        /// A portable property name
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// The new value of PropertyName
        /// </summary>
        object NewValue { get; }
    }
}
