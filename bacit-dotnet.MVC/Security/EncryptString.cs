using System.Security.Cryptography;
using System.Text;

namespace bacit_dotnet.MVC.Security
{
    public static class EncryptString
    {
        public static string Encrypt(string s)
        {
            using var sha265 = SHA256.Create();
            byte[] bytes = sha265.ComputeHash(Encoding.UTF8.GetBytes(s));

            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
