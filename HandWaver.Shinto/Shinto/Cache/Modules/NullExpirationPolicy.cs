using System;

namespace Shinto.Cache.Modules
{
    /// <summary>
    /// Does nothing, doesn't expire
    /// </summary>
    public class NullExpirationPolicy : ICacheExpirationPolicy
    {
        public void ItemAdded(object key, object item)
        {
        }

        public void ItemAccessed(object key, object item)
        {
        }

        public void SetProvider(ICacheProvider prover)
        {
        }


        public void ItemRemoved(object key, object item)
        {
        }
    }
}
