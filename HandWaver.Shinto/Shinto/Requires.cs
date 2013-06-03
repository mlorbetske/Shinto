// http://creativecommons.org/licenses/by/3.0/
// Please consider giving me a link or shout out if you use this code 
// http://www.damonpayne.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto
{
    /// <summary>
    /// Cross platform Assertion semantics
    /// </summary>
    public class Requires
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="test"></param>
        /// <param name="errMsg"></param>
        public static void NotNull(object test, string errMsg)
        {
            if (null == test)
            {
                throw new ArgumentException(errMsg);
            }
        }

        public static void IsNull(object test, string errMsg)
        {
            if (null != test)
            {
                throw new ArgumentException(errMsg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="test"></param>
        /// <param name="errMsg"></param>
        public static void IsTrue(bool test, string errMsg)
        {
            if (!test)
            {
                throw new ArgumentException(errMsg);
            }
        }

    }
}
