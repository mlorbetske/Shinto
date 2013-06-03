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
    /// An Attribute that models a specific Property of an Entity
    /// </summary>
    public interface IAttribute
    {
        /// <summary>
        /// A friendly name for this Attribute
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        AttributeValueTypes ValueType { get; set; }
    }
}
