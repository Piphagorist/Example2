using System.Security.Cryptography;
using System.Text;

namespace SG2.Base.Extensions
{
    public static class StringExtensions
    {
        public static string GetMD5(this string input)
        {
            StringBuilder hash = new();
            MD5CryptoServiceProvider md5Provider = new();
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            foreach (byte t in bytes)
                hash.Append(t.ToString("x2"));
            
            return hash.ToString();
        }

        public static string GetPercents(this float progress)
        {
            return (progress * 100.0f).ToString("f0") + "%";
        }
    }
}