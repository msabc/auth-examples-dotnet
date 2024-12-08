using AuthenticationExamples.Application.Models.Request.BasicAuthentication;
using Microsoft.Extensions.Primitives;

namespace AuthenticationExamples.Application.Auth.BasicAuthentication
{
    public interface IBasicAuthService
    {
        Task RegisterAsync(RegisterRequest request);

        Task<bool> AuthenticateAsync(StringValues authHeaders);
    }
}
