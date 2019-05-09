namespace Astron.Logging.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string s)
            => s.Substring(0, 1).ToUpper() +
               s.Substring(1, s.Length - 1);
    }
}
