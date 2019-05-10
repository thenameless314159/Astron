using System;
using System.Collections.Generic;
using System.Text;

namespace Astron.Logging.IntegrationTests
{
    public static class Displayer
    {
        public static bool IsRegistered<T>() => DisplayerCache<T>.Display != default;

        public static void Register<T>(Func<T, string> toString) => DisplayerCache<T>.Display = toString;

        public static string Of<T>(T instance) => DisplayerCache<T>.Display(instance);

        static class DisplayerCache<T>
        {
            public static Func<T, string> Display { get; set; }
        }
    }
}
