using System;

namespace Shinto.Cache.Modules
{
    public class AbsoluteExpirationPolicy : TimeBasedExpirationPolicy
    {
        public AbsoluteExpirationPolicy(object key, object target, DateTime absoluteExpiration)
            : base(key, target)
        {
            _expiration = absoluteExpiration;
        }

        DateTime _expiration;

        protected override bool ShouldRemove(DateTime date)
        {
            bool shouldRemove = date > _expiration;
            if (shouldRemove)
            {
                string message = string.Format("Removing {0} due to AbsoluteExpirationPolicy of {1}", _key, _expiration);
                TimeBasedExpirationPolicy.Log.Debug(message); 
            }
            return shouldRemove;
        }
    }
}
