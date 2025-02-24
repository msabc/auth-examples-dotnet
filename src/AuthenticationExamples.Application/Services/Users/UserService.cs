using AuthenticationExamples.Application.Models.Enums;
using AuthenticationExamples.Application.Models.Request.BasicAuthentication;
using AuthenticationExamples.Application.Services.Password;
using AuthenticationExamples.Domain.Interfaces.Repositories;
using AuthenticationExamples.Domain.Models.Tables;

namespace AuthenticationExamples.Application.Services.Users
{
    public class UserService(IUserRepository userRepository, IPasswordService passwordService) : IUserService
    {
        public async Task RegisterAsync(RegisterRequest request)
        {
            (byte[] passwordHash, byte[] passwordSalt) = passwordService.CreatePasswordHash(request.Password);

            await userRepository.AddAsync(new User()
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Username = request.Username,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
                RoleId = (int)Roles.User
            });
        }
    }
}
