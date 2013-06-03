using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace System.Data
{
    public static class IDataReaderExtensions
    {
        /// <summary>
        /// Cast the value at the given ordinal to the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T GetValue<T>(this IDataReader reader, int index)
        {
            return GetValue<T>(reader, index, default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public static T GetValue<T>(this IDataReader reader, int index, T @default)
        {
            object value = reader.GetValue(index);
            if (DBNull.Value.Equals(value))
            {
                return @default;
            }
            return (T)reader.GetValue(index);
            
        }
    }
}
