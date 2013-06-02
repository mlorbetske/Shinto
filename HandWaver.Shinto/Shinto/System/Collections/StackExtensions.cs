using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.System.Collections
{
    public static class StackExtensions
    {
        public static bool TryPop<T>(this Stack<T> stack, out T result)
        {
            bool success = false;
            result = default(T);
            if (stack.Count > 0)
            {
                result = stack.Pop();
            }

            return success;
        }
    }
}
