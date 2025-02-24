using AuthenticationExamples.Domain.Models.Tables;
using Microsoft.Extensions.Primitives;

namespace AuthenticationExamples.Application.Services.BasicAuthentication
{
    public interface IBasicAuthenticationService
    {
        Task<User> AuthenticateAsync(StringValues authHeaders);
    }
}
