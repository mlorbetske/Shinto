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
    /// Platform-specific plug in for the initializer to use for composition/injection
    /// </summary>
    public interface ICompositionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        void SatisfyImports(object part);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStaticType"></typeparam>
        void SatisfyImports<TStaticType>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetSingleExport<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllExports<T>();
    }
}
