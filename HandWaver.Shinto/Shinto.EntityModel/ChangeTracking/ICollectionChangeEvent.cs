using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Shinto.EntityModel.ChangeTracking
{
    /// <summary>
    /// A single operation modifying an Entity Collection
    /// </summary>
    public interface ICollectionChangeEvent : IChangeEvent
    {
        string CollectionPropertyName { get; set; }

        NotifyCollectionChangedAction Action { get; set; }

        IEnumerable<object> NewItems { get; set; }

        IEnumerable<object> OldItems { get; set; }
    }
}
