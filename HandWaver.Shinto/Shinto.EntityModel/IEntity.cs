// http://creativecommons.org/licenses/by/3.0/
// Please consider giving me a link or shout out if you use this code 
// http://www.damonpayne.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.EntityModel
{
    /// <summary>
    /// An Entity has a key representing its identity. Implementors should consider overriding Equals and GetHashCode
    /// in terms of this key
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Return the Id
        /// </summary>
        /// <returns></returns>
        object GetId();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">New Id value</param>
        void SetId(object value);
    }
}
