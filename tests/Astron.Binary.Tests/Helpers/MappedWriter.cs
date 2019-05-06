using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Astron.Binary.Tests.Helpers
{
    public class MappedWriter : BinaryWriter
    {
        public byte[] GetData()
        {
            var data = (BaseStream as MemoryStream)?.GetBuffer();
            Array.Resize(ref data, (int)BaseStream.Position);
            return data;
        }

        static MappedWriter()
        {
            WriteMapper<bool>.WriteValue = (w, v) => w.Write(v);
            WriteMapper<byte>.WriteValue = (w, v) => w.Write(v);
            WriteMapper<sbyte>.WriteValue = (w, v) => w.Write(v);
            WriteMapper<short>.WriteValue = (w, v) => w.Write(BinaryPrimitives.ReverseEndianness(v));
            WriteMapper<ushort>.WriteValue = (w, v) => w.Write(BinaryPrimitives.ReverseEndianness(v));
            WriteMapper<int>.WriteValue = (w, v) => w.Write(BinaryPrimitives.ReverseEndianness(v));
            WriteMapper<uint>.WriteValue = (w, v) => w.Write(BinaryPrimitives.ReverseEndianness(v));
            WriteMapper<long>.WriteValue = (w, v) => w.Write(BinaryPrimitives.ReverseEndianness(v));
            WriteMapper<ulong>.WriteValue = (w, v) => w.Write(BinaryPrimitives.ReverseEndianness(v));

            WriteMapper<float>.WriteValue = (w, v) =>
                w.Write(BinaryPrimitives.ReverseEndianness(BitConverter.SingleToInt32Bits(v)));

            WriteMapper<double>.WriteValue = (w, v)
                => w.Write(BinaryPrimitives.ReverseEndianness(BitConverter.DoubleToInt64Bits(v)));

            WriteMapper<byte>.WriteValues = (w, v) => w.Write(v);

            WriteMapper<sbyte>.WriteValues = (w, v) =>
            {
                for (var i = 0; i < v.Length; i++)
                    w.Write(v[i]);
            };

            WriteMapper<short>.WriteValues = (w, v) =>
            {
                for (var i = 0; i < v.Length; i++)
                    w.Write(BinaryPrimitives.ReverseEndianness(v[i]));
            };

            WriteMapper<ushort>.WriteValues = (w, v) =>
            {
                for (var i = 0; i < v.Length; i++)
                    w.Write(BinaryPrimitives.ReverseEndianness(v[i]));
            };

            WriteMapper<int>.WriteValues = (w, v) =>
            {
                for (var i = 0; i < v.Length; i++)
                    w.Write(BinaryPrimitives.ReverseEndianness(v[i]));
            };

            WriteMapper<uint>.WriteValues = (w, v) =>
            {
                for (var i = 0; i < v.Length; i++)
                    w.Write(BinaryPrimitives.ReverseEndianness(v[i]));
            };

            WriteMapper<long>.WriteValues = (w, v) =>
            {
                for (var i = 0; i < v.Length; i++)
                    w.Write(BinaryPrimitives.ReverseEndianness(v[i]));
            };

            WriteMapper<ulong>.WriteValues = (w, v) =>
            {
                for (var i = 0; i < v.Length; i++)
                    w.Write(BinaryPrimitives.ReverseEndianness(v[i]));
            };

            WriteMapper<float>.WriteValues = (w, v) =>
            {
                for (var i = 0; i < v.Length; i++)
                    w.Write(BinaryPrimitives.ReverseEndianness(BitConverter.SingleToInt32Bits(v[i])));
            };

            WriteMapper<double>.WriteValues = (w, v) =>
            {
                for (var i = 0; i < v.Length; i++)
                    w.Write(BinaryPrimitives.ReverseEndianness(BitConverter.DoubleToInt64Bits(v[i])));
            };
        }

        public MappedWriter(MemoryStream s) : base(s)
        {
        }

        public void WriteValue<T>(T value) => WriteMapper<T>.WriteValue(this, value);
        public void WriteValues<T>(T[] values) => WriteMapper<T>.WriteValues(this, values);

        public void WriteUtf8(string value)
        {
            WriteValue(value.Length);
            Write(Encoding.UTF8.GetBytes(value));
        }

        public void WriteAscii(string value)
        {
            WriteValue(value.Length);
            Write(Encoding.ASCII.GetBytes(value));
        }
    }

    internal static class WriteMapper<T>
    {
        public static Action<BinaryWriter, T> WriteValue { get; internal set; }
        public static Action<BinaryWriter, T[]> WriteValues { get; internal set; }
    }
}
