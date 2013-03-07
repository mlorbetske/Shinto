// http://creativecommons.org/licenses/by/3.0/
// Please consider giving me a link or shout out if you use this code 
// http://www.damonpayne.com
using Shinto.EntityModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Shinto.EntityModel
{
    public interface ITrackingEntityCollection<TEntity> : IEntityCollection<TEntity> where TEntity : IEntityWithChangeTracking
    {
    }
}
