using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Shinto.Serialization
{
    public class Serializer
    {
        public static void Serialize<T>(object graph, Stream s)
        {
            var ser = new DataContractSerializer(typeof(T));
            ser.WriteObject(s, graph);
        }

        public static T DeSerialize<T>(Stream s)
        {
            T graph = default(T);
            var ser = new DataContractSerializer(typeof(T));
            graph = (T)ser.ReadObject(s);

            return graph;
        }
    }
}
