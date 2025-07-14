using System.Security.Cryptography;
using System.Text;

namespace LinkBL.ModelBL.TableWithUrlBL
{
    public static class UrlGenerator
    {
        private static readonly char[] _chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        public static string Generate(int length = 5)
        {
            var data = new byte[length];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(data);

            var result = new StringBuilder(length);
            foreach (var b in data)
                result.Append(_chars[b % _chars.Length]);

            return result.ToString();
        }
    }
}
