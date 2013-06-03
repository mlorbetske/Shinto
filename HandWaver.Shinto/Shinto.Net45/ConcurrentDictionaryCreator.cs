using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shinto
{
    public class ConcurrentDictionaryCreator : IConcurrentDictionaryCreator
    {
        public IConcurrentDictionary<TKey, TValue> Create<TKey, TValue>()
        {
            return new ConcurrentDictionaryFacade<TKey,TValue>();
        }
    }
}
