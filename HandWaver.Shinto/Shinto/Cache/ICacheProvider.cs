using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.Cache
{
    /// <summary>
    /// Application-scoped Cache
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Add or replace
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        void Put(object key, object item);

        /// <summary>
        /// Add or replace
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <param name="expirationPolicy"></param>
        void Put(object key, object item, ICacheExpirationPolicy expirationPolicy);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void Remove(object key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(object key);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>the item, or default(T) if not present</returns>
        T Get<T>(object key);

        /// <summary>
        /// Everything must go!
        /// </summary>
        void Clear();

    }
}
