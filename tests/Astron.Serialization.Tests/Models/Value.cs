using System;
using System.Collections.Generic;
using System.Text;
using Astron.Binary.Writer;

namespace Astron.Serialization.Tests.Models
{
    public class Value
    {
        public byte B { get; set; }
        public short S { get; set; }
        public int I { get; set; }
        public long L { get; set; }

        public void Serialize(IWriter writer)
        {
            writer.WriteValue(B);
            writer.WriteValue(S);
            writer.WriteValue(I);
            writer.WriteValue(L);
        }
    }
}
