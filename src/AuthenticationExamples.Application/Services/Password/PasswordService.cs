using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace AuthenticationExamples.Application.Services.Password
{
    public class PasswordService : IPasswordService
    {
        public (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string input)
        {
            byte[] passwordSalt = RandomNumberGenerator.GetBytes(128 / 8);

            byte[] passwordHash = HashPassword(input, passwordSalt);

            return (passwordHash, passwordSalt);
        }

        public bool VerifyPassword(string input, byte[] passwordHash, byte[] passwordSalt)
        {
            byte[] generatedHash = HashPassword(input, passwordSalt);

            return passwordHash.SequenceEqual(generatedHash);
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            );
        }
    }
}
