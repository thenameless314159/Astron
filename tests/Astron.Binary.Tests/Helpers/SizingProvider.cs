using Astron.Size;

namespace Astron.Binary.Tests.Helpers
{
    public static class SizingProvider
    {
        public static readonly ISizing Sizing = new SizingBuilder().Register(new StringSizeStorage()).Build();
    }
}
