using Astron.Size;
using System;
using System.Collections.Generic;
using System.Text;

namespace Astron.Binary.Tests.Helpers
{
    public static class SizingProvider
    {
        public static readonly ISizing Sizing = new SizingBuilder().Register(new StringSizeStorage()).Build();
    }
}
