using Shinto.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Shinto.PresentationModel
{
    /// <summary>
    /// A base class for change notification and other property behaviors
    /// </summary>
    [DataContract]
    public class Bindable : INotifyPropertyChanged, IHavePropertyChangeBehaviors
    {
        public Bindable()
        {
            _behaviors = new List<IPropertyChangeBehavior>();
            _behaviors.Add( new INotifyPropertyChangedBehavior(this, p => OnPropertyChanged(p) ) );
        }

        List<IPropertyChangeBehavior> _behaviors; 


        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T value, T newValue, string propertyName)
        {
            if (!Object.Equals(value, newValue))
            {
                value = newValue;
                OnPropertyChanged(propertyName);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public List<IPropertyChangeBehavior> Behaviors
        {
            get { return _behaviors; }
        }
    }
}
