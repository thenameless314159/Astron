using System;
using System.Text;
using Astron.Binary.Reader;
using Astron.Binary.Writer;

namespace Astron.Binary.Storage
{
    public class AsciiBinaryStorage : IBinaryStorage<string>
    {
        public Func<IReader, string> ReadValue => reader =>
        {
            var length = reader.ReadValue<int>();

            if (length < 1) return string.Empty;
            var encodedStr = reader.GetSlice(length);
            reader.Advance(length);
            return Encoding.ASCII.GetString(encodedStr.Span);
        };

        public Action<IWriter, string> WriteValue => (writer, value) =>
        {
            if (value == string.Empty) return;

            var encodedStr = Encoding.ASCII.GetBytes(value);
            writer.WriteValue(encodedStr.Length);
            writer.WriteBytes(encodedStr);
        };
    }
}
