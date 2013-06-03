using Shinto.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.Entities
{
    /// <summary>
    /// An entity with a parameterized key type
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class Entity<TKey> :Entity, IEntity<TKey>
    {
        public TKey Id
        {
            get
            {
                var key = GetId();
                if (null == key)
                {
                    return default(TKey);
                }
                return (TKey)key;
            }
            //Need to allow this for some serialization stuff unfortunately
            set
            {
                SetId(value);
            }
        }

        public override bool Equals(object obj)
        {
            var rightOp = obj as Entity<TKey>;
            return base.Equals(rightOp);
        }
    }
}
