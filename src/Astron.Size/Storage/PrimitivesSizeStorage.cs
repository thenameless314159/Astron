namespace Astron.Size.Storage
{
    public class BoolSizeStorage : ISizeStorage<bool>
    {
        public int Value => sizeof(bool);
    }

    public class Int8SizeStorage : ISizeStorage<sbyte>
    {
        public int Value => sizeof(sbyte);
    }

    public class UInt8SizeStorage : ISizeStorage<byte>
    {
        public int Value => sizeof(byte);
    }

    public class Int16SizeStorage : ISizeStorage<short>
    {
        public int Value => sizeof(short);
    }

    public class UInt16SizeStorage : ISizeStorage<ushort>
    {
        public int Value => sizeof(ushort);
    }

    public class Int32SizeStorage : ISizeStorage<int>
    {
        public int Value => sizeof(int);
    }

    public class UInt32SizeStorage : ISizeStorage<uint>
    {
        public int Value => sizeof(uint);
    }

    public class Int64SizeStorage : ISizeStorage<long>
    {
        public int Value => sizeof(long);
    }

    public class UInt64SizeStorage : ISizeStorage<ulong>
    {
        public int Value => sizeof(ulong);
    }

    public class FloatSizeStorage : ISizeStorage<float>
    {
        public int Value => sizeof(float);
    }

    public class DoubleSizeStorage : ISizeStorage<double>
    {
        public int Value => sizeof(double);
    }
}
