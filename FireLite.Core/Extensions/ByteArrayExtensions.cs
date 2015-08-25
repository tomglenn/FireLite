using System.Text;

namespace FireLite.Core.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string GetString(this byte[] bytes)
        {
            var encoder = new UTF8Encoding();
            return encoder.GetString(bytes);
        }
    }
}
