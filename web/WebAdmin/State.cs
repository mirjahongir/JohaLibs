using Microsoft.IdentityModel.Tokens;

using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using WebAdmin.Models.Account;

namespace WebAdmin
{
    public static class State
    {
        #region Function
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
        public static ClaimsIdentity GenerateClaim(this User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim("id", user.Id),

                };
            foreach (var i in user?.Projects)
            {
                if (!string.IsNullOrEmpty(i.Id))
                    claims.Add(new Claim("project", i.Id));
            }

            ClaimsIdentity claimsIdentity =
              new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                  ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;

        }
        #endregion
        #region Auth
        public const string ISSUER = "WebAdminToken"; // издатель токена
        public const string AUDIENCE = "AdminWeb"; // потребитель токена
        const string KEY = "jpZXRGi3bJM7OIACM9EBTtHeTGWM0Pks!123";   // ключ для шифрации
        public const int LIFETIME = 999; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
        #endregion
    }
}
