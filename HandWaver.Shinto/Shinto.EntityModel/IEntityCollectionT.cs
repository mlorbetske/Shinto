// http://creativecommons.org/licenses/by/3.0/
// Please consider giving me a link or shout out if you use this code 
// http://www.damonpayne.com
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Shinto.EntityModel
{
    /// <summary>
    /// A collection of Entities. INCC in order to allow implementors to keep track of relationships.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityCollection<TEntity> : IList<TEntity>, INotifyCollectionChanged where TEntity : IEntity
    {
    }
}
