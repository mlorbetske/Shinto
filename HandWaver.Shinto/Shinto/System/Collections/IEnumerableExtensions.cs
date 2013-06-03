using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace System.Collections
{
    /// <summary>
    /// LINQ helpers for non-generic IEnumerables
    /// </summary>
    public static class IEnumerableExtensions
    {
        public static void ForEach(this IEnumerable src, Action<object> action)
        {
            foreach (object o in src)
            {
                action(o);
            }
        }
    }
}
