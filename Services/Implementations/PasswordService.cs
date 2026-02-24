using System.Security.Cryptography;
using System.Text;
using Homecare_Dotnet.Services.Interfaces;

namespace Homecare_Dotnet.Services
{
    public class PasswordService : IPasswordService
    {
        public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using var hmac = new HMACSHA512();

            passwordSalt = Convert.ToBase64String(hmac.Key);

            passwordHash = Convert.ToBase64String(
                hmac.ComputeHash(Encoding.UTF8.GetBytes(password))
            );
        }

        public bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            var key = Convert.FromBase64String(storedSalt);

            using var hmac = new HMACSHA512(key);

            var computedHash = Convert.ToBase64String(
                hmac.ComputeHash(Encoding.UTF8.GetBytes(password))
            );

            return computedHash == storedHash;
        }
    }
}
