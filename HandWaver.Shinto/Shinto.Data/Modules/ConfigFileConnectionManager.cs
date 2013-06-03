using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Configuration;

namespace Shinto.Data.Modules
{
    [Export(typeof(IConnectionManager))]
    public class ConfigFileConnectionManager : IConnectionManager
    {
        public string GetConnectionString(string key)
        {
            var section = ConfigurationManager.ConnectionStrings[key];
            if(null == section)
            {
                throw new ArgumentException("Could not find a connection string named " + key);
            }
            return section.ConnectionString;
        }
    }
}
