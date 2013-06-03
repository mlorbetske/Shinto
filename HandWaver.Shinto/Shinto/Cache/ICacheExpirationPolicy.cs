using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.Cache
{
    /// <summary>
    /// Tell an ICacheProvider when items can be removed from the cache
    /// </summary>
    public interface ICacheExpirationPolicy
    {
        void SetProvider(ICacheProvider provider);
        void ItemAdded(object key, object item);
        void ItemAccessed(object key, object item);
        void ItemRemoved(object key, object item);
    }
}
