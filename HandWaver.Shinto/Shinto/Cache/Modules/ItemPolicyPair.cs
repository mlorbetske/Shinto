using System;


namespace Shinto.Cache.Modules
{
    /// <summary>
    /// Store an item's cache expiration policy with it
    /// </summary>
    public class ItemPolicyPair : IDisposable
    {
        public ItemPolicyPair(object item, ICacheExpirationPolicy expirationPolicy)
        {
            Item = item;
            ExpirationPolicy = expirationPolicy;
        }

        public object Item { get; private set; }

        public ICacheExpirationPolicy ExpirationPolicy { get; private set; }

        public void Dispose()
        {
            Item = null;
            ExpirationPolicy = null;
        }
    }
}
