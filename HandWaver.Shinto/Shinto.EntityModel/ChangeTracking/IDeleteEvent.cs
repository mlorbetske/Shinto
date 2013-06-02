using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.EntityModel.ChangeTracking
{
    /// <summary>
    /// A marker interface, until I think of a better way to model this
    /// </summary>
    public interface IDeleteEvent : IChangeEvent
    {
    }
}
