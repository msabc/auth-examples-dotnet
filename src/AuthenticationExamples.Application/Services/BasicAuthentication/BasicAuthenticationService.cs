using System.Text;
using AuthenticationExamples.Application.Services.Password;
using AuthenticationExamples.Domain.Exceptions;
using AuthenticationExamples.Domain.Interfaces.Repositories;
using AuthenticationExamples.Domain.Models.Tables;
using Microsoft.Extensions.Primitives;

namespace AuthenticationExamples.Application.Services.BasicAuthentication
{
    public class BasicAuthenticationService(IUserRepository userRepository, IPasswordService passwordService) : IBasicAuthenticationService
    {
        private const string BASIC_AUTH_SCHEME_PREFIX = "Basic";

        public async Task<User> AuthenticateAsync(StringValues authHeaders)
        {
            var basicAuthHeaders = authHeaders
                .Where(x => !string.IsNullOrEmpty(x) && x.StartsWith(BASIC_AUTH_SCHEME_PREFIX))
                .ToList();

            if (basicAuthHeaders.Count == 0)
                throw new CustomHttpException("No Basic authentication headers received.", System.Net.HttpStatusCode.BadRequest);

            string basicAuthHeader = basicAuthHeaders.First()!;

            string basicAuthValue = basicAuthHeader[BASIC_AUTH_SCHEME_PREFIX.Length..];

            (string username, string password) = ExtractUserNameAndPassword(basicAuthValue);

            var selectedUser = await userRepository.GetByUsernameAsync(username);

            if (selectedUser == null)
                throw new CustomHttpException("Invalid username", System.Net.HttpStatusCode.NotFound);

            if (!passwordService.VerifyPassword(password, selectedUser.PasswordHash, selectedUser.PasswordSalt))
                throw new CustomHttpException("Invalid credentials.", System.Net.HttpStatusCode.Unauthorized);

            return selectedUser;
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

            Encoding encoding = Encoding.ASCII;
            encoding = (Encoding)encoding.Clone();

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
