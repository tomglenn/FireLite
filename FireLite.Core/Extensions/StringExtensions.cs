using System.Text;

namespace FireLite.Core.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetBytes(this string str)
        {
            var encoder = new UTF8Encoding();
            return encoder.GetBytes(str);
        }
    }
}
