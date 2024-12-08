using AuthenticationExamples.Domain.Models.Tables;

namespace AuthenticationExamples.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);

        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByUsernameAsync(string email);
    }
}
