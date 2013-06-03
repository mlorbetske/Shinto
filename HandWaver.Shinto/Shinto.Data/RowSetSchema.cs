using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto.Data
{
    public class RowSetSchema
    {
        public RowSetSchema(Dictionary<int, string> indexToColumnName)
        {
            _indexToColumnName = indexToColumnName;
            Build();
        }

        public RowSetSchema(Dictionary<string, int> columnNameToIndex)
        {
            _columnNameToIndex = columnNameToIndex;
            Build();
        }

        void Build()
        {
            if (null != _indexToColumnName)
            {
                _columnNameToIndex = new Dictionary<string, int>();
                foreach (int key in _indexToColumnName.Keys)
                {
                    string colName = _indexToColumnName[key];
                    _columnNameToIndex[colName] = key;
                }
            }
            else if (null != _columnNameToIndex)
            {
                _indexToColumnName = new Dictionary<int, string>();
                foreach (string key in _columnNameToIndex.Keys)
                {
                    int colIdx = _columnNameToIndex[key];
                    _indexToColumnName[colIdx] = key;
                }
            }
        }

        Dictionary<int, string> _indexToColumnName;

        Dictionary<string, int> _columnNameToIndex;

        public string this[int index]
        {
            get
            {
                return _indexToColumnName[index];
            }
        }

        public int this[string col]
        {
            get
            {
                return _columnNameToIndex[col];
            }
        }

        public int ColumnCount
        {
            get
            {
                return _indexToColumnName.Count;
            }
        }


    }
}
