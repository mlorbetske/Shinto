using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinto
{
    public class ConcurrentDictionaryProvider
    {
        public static IConcurrentDictionaryCreator Creator { get; set; }

        public static IConcurrentDictionary<TKey, TValue> Create<TKey,TValue>()
        {
            Requires.NotNull(Creator, "Provide an IConcurrentDictionaryCreator");
            return Creator.Create<TKey,TValue>();
        }
    }
}
