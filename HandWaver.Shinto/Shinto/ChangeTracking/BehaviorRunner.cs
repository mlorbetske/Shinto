using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.ChangeTracking
{
    /// <summary>
    /// Execute IPropertyChangeBehavior on behalf the the owning instance
    /// </summary>
    public class BehaviorRunner
    {
        public BehaviorRunner(IHavePropertyChangeBehaviors owner)
        {
            _owner = owner;
        }

        IHavePropertyChangeBehaviors _owner;

        public void PropertyChanged<T>(object owner, T oldVal, T newVal, string propertyName)
        {
            var behaviors = _owner.Behaviors;
            for (int i = 0; i < behaviors.Count; ++i)
            {
                bool @continue = behaviors[i].PropertyChanged<T>(owner, oldVal, newVal, propertyName);
                if (!@continue)
                {
                    break;
                }
            }
        }

    }
}
