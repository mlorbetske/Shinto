using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConnectionManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetConnectionString(string name);        
    }
}
