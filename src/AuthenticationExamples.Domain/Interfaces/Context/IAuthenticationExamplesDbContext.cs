using AuthenticationExamples.Domain.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace BasicAuth.Domain.Interfaces.Context
{
    public interface IAuthenticationExamplesDbContext
    {
        DbSet<User> User { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
