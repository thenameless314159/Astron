using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace Astron.Binary.Tests.Helpers
{
    public class MappedReader : BinaryReader
    {
        static MappedReader()
        {
            ReadMapper<bool>.ReadValue = r => r.ReadByte() != 0;
            ReadMapper<byte>.ReadValue = r => r.ReadByte();
            ReadMapper<sbyte>.ReadValue = r => r.ReadSByte();
            ReadMapper<short>.ReadValue = r => BinaryPrimitives.ReverseEndianness(r.ReadInt16());
            ReadMapper<ushort>.ReadValue = r => BinaryPrimitives.ReverseEndianness(r.ReadUInt16());
            ReadMapper<int>.ReadValue = r => BinaryPrimitives.ReverseEndianness(r.ReadInt32());
            ReadMapper<uint>.ReadValue = r => BinaryPrimitives.ReverseEndianness(r.ReadUInt32());
            ReadMapper<long>.ReadValue = r => BinaryPrimitives.ReverseEndianness(r.ReadInt64());
            ReadMapper<ulong>.ReadValue = r => BinaryPrimitives.ReverseEndianness(r.ReadUInt64());

            ReadMapper<float>.ReadValue = r => 
                BitConverter.Int32BitsToSingle(BinaryPrimitives.ReverseEndianness(r.ReadInt32()));

            ReadMapper<double>.ReadValue = r => 
                BitConverter.Int64BitsToDouble(BinaryPrimitives.ReverseEndianness(r.ReadInt64()));

            ReadMapper<byte>.ReadValues = (r, n) => r.ReadBytes(n);

            ReadMapper<sbyte>.ReadValues = (r, n) =>
            {
                var values = new sbyte[n];
                for (var i = 0; i < n; i++)
                    values[i] = r.ReadSByte();

                return values;
            };

            ReadMapper<short>.ReadValues = (r, n) =>
            {
                var values = new short[n];
                for (var i = 0; i < n; i++)
                    values[i] = BinaryPrimitives.ReverseEndianness(r.ReadInt16());

                return values;
            };

            ReadMapper<ushort>.ReadValues = (r, n) =>
            {
                var values = new ushort[n];
                for (var i = 0; i < n; i++)
                    values[i] = BinaryPrimitives.ReverseEndianness(r.ReadUInt16());

                return values;
            };

            ReadMapper<int>.ReadValues = (r, n) =>
            {
                var values = new int[n];
                for (var i = 0; i < n; i++)
                    values[i] = BinaryPrimitives.ReverseEndianness(r.ReadInt32());

                return values;
            };

            ReadMapper<uint>.ReadValues = (r, n) =>
            {
                var values = new uint[n];
                for (var i = 0; i < n; i++)
                    values[i] = BinaryPrimitives.ReverseEndianness(r.ReadUInt32());

                return values;
            };

            ReadMapper<long>.ReadValues = (r, n) =>
            {
                var values = new long[n];
                for (var i = 0; i < n; i++)
                    values[i] = BinaryPrimitives.ReverseEndianness(r.ReadInt64());

                return values;
            };

            ReadMapper<ulong>.ReadValues = (r, n) =>
            {
                var values = new ulong[n];
                for (var i = 0; i < n; i++)
                    values[i] = BinaryPrimitives.ReverseEndianness(r.ReadUInt64());

                return values;
            };

            ReadMapper<float>.ReadValues = (r, n) =>
            {
                var values = new float[n];
                for (var i = 0; i < n; i++)
                    values[i] = BitConverter.Int32BitsToSingle(BinaryPrimitives.ReverseEndianness(r.ReadInt32()));

                return values;
            };

            ReadMapper<double>.ReadValues = (r, n) =>
            {
                var values = new double[n];
                for (var i = 0; i < n; i++)
                    values[i] = BitConverter.Int64BitsToDouble(BinaryPrimitives.ReverseEndianness(r.ReadInt64()));

                return values;
            };

        }

        public MappedReader(MemoryStream s) : base(s)
        {
            
        }

        public T ReadValue<T>()
            => ReadMapper<T>.ReadValue(this);
        public T[] ReadValues<T>(int n)
            => ReadMapper<T>.ReadValues(this, n);

        public string ReadUtf8()
        {
            var encodedStr = ReadBytes(ReadInt32());
            return Encoding.UTF8.GetString(encodedStr);
        }

        public string ReadAscii()
        {
            var encodedStr = ReadBytes(ReadInt32());
            return Encoding.ASCII.GetString(encodedStr);
        }
    }

    internal static class ReadMapper<T>
    {
        public static Func<BinaryReader, T> ReadValue { get; internal set; }
        public static Func<BinaryReader, int, T[]> ReadValues { get; internal set; }
    }
}
