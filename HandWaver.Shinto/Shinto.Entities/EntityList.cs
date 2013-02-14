// http://creativecommons.org/licenses/by/3.0/
// Please consider giving me a link or shout out if you use this code 
// http://www.damonpayne.com
using Shinto.EntityModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Shinto.Entities
{
    /// <summary>
    /// A list of TEntity with INCC implementation included
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityList<TEntity> : IEntityCollection<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="propertyRepresentation"></param>
        public EntityList(IEntity owner)
        {
            _source = new List<TEntity>();
            _owner = owner;
        }
        IEntity _owner;
        List<TEntity> _source;
        

        public int IndexOf(TEntity item)
        {
            return _source.IndexOf(item);
        }

        public void Insert(int index, TEntity item)
        {
            TEntity oldItem = default(TEntity);
            if(index < Count)
            {
                oldItem = this[index];
            }
            _source.Insert(index,item);
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, oldItem, index);
            OnCollectionChanged(args);
        }

        public void RemoveAt(int index)
        {
            TEntity oldItem = this[index];
            _source.RemoveAt(index);
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, null, oldItem, index);
            OnCollectionChanged(args);
        }

        public TEntity this[int index]
        {
            get
            {
                return _source[index];
            }
            set
            {
                _source[index] = value;
            }
        }

        public void Add(TEntity item)
        {
            _source.Add(item);
            int index = Count - 1;
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index);
            OnCollectionChanged(args);
        }

        public void Clear()
        {
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);

            foreach (var current in _source)
            {
                args.OldItems.Add(current);
            }
            _source.Clear();                        
            OnCollectionChanged(args);
        }

        public bool Contains(TEntity item)
        {
            return _source.Contains(item);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            _source.CopyTo(array, arrayIndex);
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);//Damn, some properties can't be assigned
            OnCollectionChanged(args);
        }

        public int Count
        {
            get { return _source.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TEntity item)
        {
            int index = _source.IndexOf(item);
            bool couldRemove = _source.Remove(item);
            if (couldRemove)
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index);
                OnCollectionChanged(args);
            }

            return couldRemove;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;

        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (null != CollectionChanged)
            {
                CollectionChanged(this, args);
            }
            // For detailed change tracking, later
            //RecordChange(args);
        }

    }
    
}
