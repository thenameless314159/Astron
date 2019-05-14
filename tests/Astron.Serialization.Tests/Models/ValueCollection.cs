using System;
using System.Collections.Generic;
using System.Text;
using Astron.Binary.Writer;

namespace Astron.Serialization.Tests.Models
{
    public class ValueCollection
    {
        public List<int> Collection { get; set; }

        public void Serialize(IWriter writer)
        {
            writer.WriteValue(Collection.Count);
            writer.WriteValues(Collection.ToArray());
        }
    }
}
