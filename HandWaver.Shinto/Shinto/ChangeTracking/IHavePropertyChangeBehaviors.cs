using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.ChangeTracking
{
    /// <summary>
    /// Implementors are able to handle, and promise to run, IPropertyChangeBehaviors 
    /// </summary>
    public interface IHavePropertyChangeBehaviors
    {
        List<IPropertyChangeBehavior> Behaviors { get; }
    }
}
