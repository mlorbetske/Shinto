using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto
{
    public interface IConcurrentDictionaryCreator
    {
        IConcurrentDictionary<TKey, TValue> Create<TKey, TValue>();
    }
}
