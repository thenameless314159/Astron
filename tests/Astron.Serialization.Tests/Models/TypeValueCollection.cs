using System;
using System.Collections.Generic;
using System.Text;
using Astron.Binary.Writer;

namespace Astron.Serialization.Tests.Models
{
    public class TypeValueCollection
    {
        public List<Value> Collection { get; set; }

        public void Serialize(IWriter writer)
        {
            writer.WriteValue(Collection.Count);
            foreach (var val in Collection)
                val.Serialize(writer);
        }
    }
}
