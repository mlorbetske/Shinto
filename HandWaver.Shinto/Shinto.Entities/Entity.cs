
// http://creativecommons.org/licenses/by/3.0/
// Please consider giving me a link or shout out if you use this code 
// http://www.damonpayne.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shinto.EntityModel;

namespace Shinto.Entities
{
    public class Entity : IEntity
    {
        public Entity()
        {
            _myHash = 0;
        }

        object _id;
        int _myHash;
        string _stringified;

        public object GetId()
        {
            return _id;
        }

        public void SetId(object value)
        {
            _id = value;
            _myHash = 0;
        }

        public override bool Equals(object rightOp)
        {
            if (null != rightOp)
            {
                if (GetType().Equals(rightOp.GetType())
                    && GetHashCode() == rightOp.GetHashCode())
                {
                    return true;
                }
            }

            return false;
        }

        protected virtual void EnsureHash()
        {
            if (0 == _myHash)
            {
                _stringified = string.Format("[{0}]{1}", GetId(), GetType());
                _myHash = _stringified.GetHashCode();
            }
        }

        public override int GetHashCode()
        {
            EnsureHash();
            return _myHash;
        }

        /// <summary>
        /// Return type + id information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            EnsureHash();
            return _stringified;
        }
    }
}
