using AuthenticationExamples.Application.Models.Enums;
using AuthenticationExamples.Application.Models.Request.BasicAuthentication;
using AuthenticationExamples.Domain.Exceptions;
using AuthenticationExamples.Domain.Interfaces.Repositories;
using AuthenticationExamples.Domain.Models.Tables;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Primitives;
using System.Security.Cryptography;
using System.Text;

namespace AuthenticationExamples.Application.Auth.BasicAuthentication
{
    public class BasicAuthService(IUserRepository userRepository) : IBasicAuthService
    {
        private const string BASIC_AUTH_SCHEME_PREFIX = "Basic";

        public async Task RegisterAsync(RegisterRequest request)
        {
            await userRepository.AddAsync(new User()
            {
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                Username = request.Username,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
                RoleId = (int)Roles.User
            });
        }

        public async Task<bool> AuthenticateAsync(StringValues authHeaders)
        {
            var basicAuthHeaders = authHeaders
                .Where(x => !string.IsNullOrEmpty(x) && x.StartsWith(BASIC_AUTH_SCHEME_PREFIX))
                .ToList();

            if (basicAuthHeaders.Count != 1)
                throw new CustomHttpException("Received too many Authorization headers.", System.Net.HttpStatusCode.BadRequest);

            string basicAuthHeader = basicAuthHeaders.First()!;

            string basicAuthValue = basicAuthHeader[BASIC_AUTH_SCHEME_PREFIX.Length..];

            (string username, string password) = ExtractUserNameAndPassword(basicAuthValue);

            var selectedUser = await userRepository.GetByUsernameAsync(username);

            return selectedUser == null
                ? throw new CustomHttpException("Invalid username", System.Net.HttpStatusCode.NotFound)
                : selectedUser.PasswordHash == HashPassword(password);
        }

        private static byte[] HashPassword(string input)
        {
            return KeyDerivation.Pbkdf2(
                    password: input,
                    salt: RandomNumberGenerator.GetBytes(128 / 8),
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8
            );
        }

        private static (string username, string password) ExtractUserNameAndPassword(string authorizationParameter)
        {
            byte[] credentialBytes;

            try
            {
                credentialBytes = Convert.FromBase64String(authorizationParameter);
            }
            catch (FormatException)
            {
                return default;
            }

            // The currently approved HTTP 1.1 specification says characters here are ISO-8859-1.
            // However, the current draft updated specification for HTTP 1.1 indicates this encoding is infrequently
            // used in practice and defines behavior only for ASCII.
            Encoding encoding = Encoding.ASCII;
            // Make a writable copy of the encoding to enable setting a decoder fallback.
            encoding = (Encoding)encoding.Clone();
            // Fail on invalid bytes rather than silently replacing and continuing.
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
            string decodedCredentials;

            try
            {
                decodedCredentials = encoding.GetString(credentialBytes);
            }
            catch (DecoderFallbackException)
            {
                return default;
            }

            int colonIndex = decodedCredentials.IndexOf(':');

            string userName = decodedCredentials[..colonIndex];
            string password = decodedCredentials[(colonIndex + 1)..];

            return (userName, password);
        }
    }
}
