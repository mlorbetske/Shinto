using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.Data
{
    public class Row
    {
        public Row(int valCount) : this (null, new object[valCount])
        {
            Requires.IsTrue(valCount > 0, "Must provide a postive non-zero value count");
        }

        public Row(object [] values) : this(null, values)
        {
        }

        public Row(RowSetSchema schema) :this(schema, new object[schema.ColumnCount])
        {
        }

        public Row(RowSetSchema schema, object[] values)
        {
            _schema = schema;
            _values = values;
        }

        RowSetSchema _schema;
        object[] _values;

        public object this[int index]
        {
            get
            {
                return _values[index];
            }
            set
            {
                _values[index] = value;
            }
        }

        public T GetValue<T>(int index)
        {
            return GetValue<T>(index, default(T) );
        }

        public T GetValue<T>(int index, T defaultValue)
        {
            if (DBNull.Value.Equals(this[index]))
            {
                return defaultValue;
            }
            return (T)this[index];
        }

        public int ValueCount
        {
            get
            {
                return _values.Length;
            }
        }
    }
}
