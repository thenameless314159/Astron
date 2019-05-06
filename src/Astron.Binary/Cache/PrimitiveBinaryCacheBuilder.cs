using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Astron.Binary.Tests")]
namespace Astron.Binary.Cache
{
    internal static class PrimitiveBinaryCacheBuilder
    {
        public static bool IsAlreadySet { get; private set; }
        public static Endianness Endianness { get; private set; }

        private static unsafe void Register<T>(
            PrimitiveBinaryCache<T>.ReadDelegate read,
            PrimitiveBinaryCache<T>.WriteDelegate write) 
            where T : unmanaged
        {
            PrimitiveBinaryCache<T>.SizeOf = sizeof(T);
            PrimitiveBinaryCache<T>.ReadValue = read;
            PrimitiveBinaryCache<T>.WriteValue = write;
            PrimitiveBinaryCache<T>.CompleteRegistration();
        }

        static PrimitiveBinaryCacheBuilder() // register readByte
        {
            Register(src => src[0], (dst, val) => dst[0] = val);
            Register(src => unchecked((sbyte) src[0]), (dst, val) => dst[0] = unchecked((byte) val));
            Register(src => src[0] != 0, (dst, val) => dst[0] = dst[0] = val ? (byte) 1 : (byte) 0);
        }

        public static void RegisterAllBigEndian()
        {
            Register(BinaryPrimitives.ReadInt16BigEndian, BinaryPrimitives.WriteInt16BigEndian);
            Register(BinaryPrimitives.ReadInt32BigEndian, BinaryPrimitives.WriteInt32BigEndian);
            Register(BinaryPrimitives.ReadInt64BigEndian, BinaryPrimitives.WriteInt64BigEndian);
            Register(BinaryPrimitives.ReadUInt16BigEndian, BinaryPrimitives.WriteUInt16BigEndian);
            Register(BinaryPrimitives.ReadUInt32BigEndian, BinaryPrimitives.WriteUInt32BigEndian);
            Register(BinaryPrimitives.ReadInt64BigEndian, BinaryPrimitives.WriteInt64BigEndian);
            Register(BinaryPrimitives.ReadUInt64BigEndian, BinaryPrimitives.WriteUInt64BigEndian);
            Register(
                src => BitConverter.Int32BitsToSingle(BinaryPrimitives.ReadInt32BigEndian(src)),
                (dst, val) => BinaryPrimitives.WriteInt32BigEndian(dst, BitConverter.SingleToInt32Bits(val)));
            Register(
                src => BitConverter.Int64BitsToDouble(BinaryPrimitives.ReadInt64BigEndian(src)),
                (dst, val) => BinaryPrimitives.WriteInt64BigEndian(dst, BitConverter.DoubleToInt64Bits(val)));
            

            IsAlreadySet = true;
            Endianness = Endianness.BigEndian;
        }

        public static void RegisterAllLittleEndian()
        {
            Register(BinaryPrimitives.ReadInt16LittleEndian, BinaryPrimitives.WriteInt16LittleEndian);
            Register(BinaryPrimitives.ReadInt32LittleEndian, BinaryPrimitives.WriteInt32LittleEndian);
            Register(BinaryPrimitives.ReadInt64LittleEndian, BinaryPrimitives.WriteInt64LittleEndian);
            Register(BinaryPrimitives.ReadUInt16LittleEndian, BinaryPrimitives.WriteUInt16LittleEndian);
            Register(BinaryPrimitives.ReadUInt32LittleEndian, BinaryPrimitives.WriteUInt32LittleEndian);
            Register(BinaryPrimitives.ReadInt64LittleEndian, BinaryPrimitives.WriteInt64LittleEndian);
            Register(BinaryPrimitives.ReadUInt64LittleEndian, BinaryPrimitives.WriteUInt64LittleEndian);
            Register(
                src => BitConverter.Int32BitsToSingle(BinaryPrimitives.ReadInt32LittleEndian(src)),
                (dst, val) => BinaryPrimitives.WriteInt32LittleEndian(dst, BitConverter.SingleToInt32Bits(val)));
            Register(
                src => BitConverter.Int64BitsToDouble(BinaryPrimitives.ReadInt64LittleEndian(src)),
                (dst, val) => BinaryPrimitives.WriteInt64LittleEndian(dst, BitConverter.DoubleToInt64Bits(val)));
            
            IsAlreadySet = true;
            Endianness = Endianness.LittleEndian;
        }
    }
}
