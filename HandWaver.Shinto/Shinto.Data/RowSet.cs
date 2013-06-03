using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.Data
{
    /// <summary>
    /// A light version of DataTable with no performance-robbing BS
    /// </summary>
    public class RowSet
    {
        public RowSet() : this(null)
        {
        }

        public RowSet(Dictionary<int, string> schema)
        {
            if (null != schema)
            {
                _schema = new RowSetSchema(schema);
            }
            _rows = new List<Row>();
        }



        List<string> _columns;
        List<Row> _rows;
        RowSetSchema _schema;


        public List<string> Columns
        {
            get
            {
                return _columns;
            }
        }

        public List<Row> Rows
        {
            get
            {
                return _rows;
            }
        }

        public void AddRow(object[] values)
        {
            var row = new Row(_schema, values);
            AddRow(row);
        }

        public void AddRow(Row newRow)
        {
            Rows.Add(newRow);
        }



    }
}
