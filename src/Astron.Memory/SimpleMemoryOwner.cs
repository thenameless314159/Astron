﻿using System;
using System.Buffers;

namespace Astron.Memory
{
    public sealed class SimpleMemoryOwner<T> : IMemoryOwner<T>
    {
        public static IMemoryOwner<T> Empty { get; } = new SimpleMemoryOwner<T>(Array.Empty<T>());
        public SimpleMemoryOwner(Memory<T> memory) => Memory = memory;

        public Memory<T> Memory { get; }
        public void Dispose() { }
    }
}
