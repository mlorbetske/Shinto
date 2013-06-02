using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Shinto.ChangeTracking
{
    public class INotifyPropertyChangedBehavior : IPropertyChangeBehavior
    {
        public INotifyPropertyChangedBehavior(INotifyPropertyChanged owner, Action<string> propertyChanged)
        {
            _owner = owner;
            _propertyChanged = propertyChanged;
        }

        INotifyPropertyChanged _owner;
        Action<string> _propertyChanged;

        public bool PropertyChanged<T>(object owner, T oldVal, T newVal, string propertyName)
        {
            _propertyChanged(propertyName);
            return true;
        }
    }
}
