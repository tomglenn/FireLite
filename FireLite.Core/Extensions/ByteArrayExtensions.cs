using System.Text;

namespace FireLite.Core.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string GetString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
