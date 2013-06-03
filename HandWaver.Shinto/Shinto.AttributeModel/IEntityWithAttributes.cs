// http://creativecommons.org/licenses/by/3.0/
// Please consider giving me a link or shout out if you use this code 
// http://www.damonpayne.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.AttributeModel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntityWithAttributes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValueType"></typeparam>
        /// <param name="attribute"></param>
        /// <returns></returns>
        TValueType GetValue<TValueType>(IAttribute<TValueType> attribute);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValueType"></typeparam>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        void SetValue<TValueType>(IAttribute<TValueType> attribute, TValueType value);

        

        //TValueType this<TValueType>[IAttribute<TValueType> attribute];
    }

    public class TEst
    {
        void foo()
        {
            IEntityWithAttributes a = null;
            IAttribute<Guid> userId = null;
            Guid myValue = a.GetValue(userId);
            

        }
    }
}
