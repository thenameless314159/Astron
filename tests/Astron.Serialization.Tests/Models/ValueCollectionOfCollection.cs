using System;
using System.Collections.Generic;
using System.Text;
using Astron.Binary.Writer;

namespace Astron.Serialization.Tests.Models
{
    public class ValueCollectionOfCollection
    {
        public List<List<int>> Collection { get; set; }

        public void Serialize(IWriter writer)
        {
            writer.WriteValue(Collection.Count);
            foreach (var collection in Collection)
            {
                writer.WriteValue(collection.Count);
                foreach (var val in collection) writer.WriteValue(val);
            }
        }
    }
}
