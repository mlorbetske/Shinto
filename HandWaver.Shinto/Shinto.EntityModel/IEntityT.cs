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
    /// An Entity with a typed key
    /// </summary>
    /// <typeparam name="TId">The id type, often a value type</typeparam>
    public interface IEntity<TId> : IEntity
    {
        /// <summary>
        /// The Entity Id of the given instance. Should be implemented in terms of Get/SetKey()
        /// </summary>
        TId Id { get; set; }
    }
}
