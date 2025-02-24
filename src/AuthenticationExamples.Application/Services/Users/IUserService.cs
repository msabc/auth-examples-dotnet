using AuthenticationExamples.Application.Models.Request.BasicAuthentication;

namespace AuthenticationExamples.Application.Services.Users
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterRequest request);
    }
}
