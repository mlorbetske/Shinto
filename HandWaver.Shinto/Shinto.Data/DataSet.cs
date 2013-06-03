using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.Data
{
    /// <summary>
    /// A lighter version of dataset with no binding-friendly constraint checking or events
    /// </summary>
    public class DataSet
    {
        public DataSet()
        {
            _tables = new List<RowSet>();
        }

        List<RowSet> _tables;

        public List<RowSet> Tables
        {
            get
            {
                return _tables;
            }
        }


    }
}
