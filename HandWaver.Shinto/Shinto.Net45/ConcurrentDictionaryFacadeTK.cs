﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shinto
{

    public class ConcurrentDictionaryFacade<TKey, TValue> : ConcurrentDictionary<TKey, TValue>, IConcurrentDictionary<TKey, TValue>
    {
    }
 

}
