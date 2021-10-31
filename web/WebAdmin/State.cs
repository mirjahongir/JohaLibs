using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAdmin
{
    public static class State
    {
        public static string Hash(this string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(text))
                sb.Append(b.ToString("X2"));

            return sb.ToString();

        }
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public const string ISSUER = "WebAdminToken"; // издатель токена
        public const string AUDIENCE = "AdminWeb"; // потребитель токена
        const string KEY = "jpZXRGi3bJM7OIACM9EBTtHeTGWM0Pks!123";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
