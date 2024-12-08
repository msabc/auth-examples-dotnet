using AuthenticationExamples.Domain.Interfaces.Repositories;
using AuthenticationExamples.Domain.Models.Tables;
using BasicAuth.Domain.Exceptions;
using BasicAuth.Domain.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationExamples.Infrastructure.Repositories
{
    public class UserRepository(IAuthenticationExamplesDbContext authenticationExamplesDbContext) : IUserRepository
    {
        public async Task AddAsync(User user)
        {
            try
            {
                var profileEntity = await authenticationExamplesDbContext.User.AddAsync(user);

                await authenticationExamplesDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, nameof(AddAsync), nameof(UserRepository), ex);
            }
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            try
            {
                return await authenticationExamplesDbContext.User.SingleOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, nameof(GetByEmailAsync), nameof(UserRepository), ex);
            }
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            try
            {
                return await authenticationExamplesDbContext.User.SingleOrDefaultAsync(x => x.Username == username);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, nameof(GetByUsernameAsync), nameof(UserRepository), ex);
            }
        }
    }
}
