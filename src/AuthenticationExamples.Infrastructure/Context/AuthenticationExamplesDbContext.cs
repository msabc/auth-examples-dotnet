using AuthenticationExamples.Domain.Models.Tables;
using BasicAuth.Domain.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationExamples.Infrastructure.Context
{
    public class AuthenticationExamplesDbContext(DbContextOptions<AuthenticationExamplesDbContext> options) 
        : DbContext(options), IAuthenticationExamplesDbContext
    {
        public DbSet<User> User { get; set; }
    }
}
