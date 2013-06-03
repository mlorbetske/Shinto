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
    /// An entry point for composition in an AppDomain
    /// </summary>
    public class Initializer
    {
        public static ICompositionProvider CompositionProvider { get; set; }


        static void EnsureProvider()
        {
            Requires.NotNull(Initializer.CompositionProvider, "You must set Initializer.CompositionProvider");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        public static void SatisfyImports(object part)
        {
            EnsureProvider();
            CompositionProvider.SatisfyImports(part);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStaticType"></typeparam>
        public static void SatisfyImports<TStaticType>()
        {
            EnsureProvider();
            CompositionProvider.SatisfyImports<TStaticType>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetSingleExport<T>()
        {
            EnsureProvider();
            return CompositionProvider.GetSingleExport<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAllExports<T>()
        {
            EnsureProvider();
            return CompositionProvider.GetAllExports<T>();
        }
    }
}
