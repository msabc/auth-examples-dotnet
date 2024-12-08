using AuthenticationExamples.Application.Auth.BasicAuthentication;
using AuthenticationExamples.Configuration;
using AuthenticationExamples.Domain.Interfaces.Repositories;
using AuthenticationExamples.Infrastructure.Context;
using AuthenticationExamples.Infrastructure.Repositories;
using BasicAuth.Domain.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AuthenticationExamples.IoC
{
    public static class BasicAuthenticationCompositionRoot
    {
        public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterSettings(configuration)
                    .RegisterDbContext()
                    .RegisterRepositories()
                    .RegisterApplicationServices();

            return services;
        }

        private static IServiceCollection RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BasicAuthSettings>(options => configuration.GetSection(nameof(BasicAuthSettings)).Bind(options));
            services.Configure<DatabaseSettings>(options => configuration.GetSection(nameof(DatabaseSettings)).Bind(options));

            return services;
        }

        private static IServiceCollection RegisterDbContext(this IServiceCollection services)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            using IServiceScope scope = serviceProvider.CreateScope();

            var databaseSettings = scope.ServiceProvider.GetService<IOptions<DatabaseSettings>>()!.Value;

            services.AddDbContext<AuthenticationExamplesDbContext>(options =>
            {
                options.UseSqlServer(databaseSettings.ConnectionString);
            });

            services.AddScoped<IAuthenticationExamplesDbContext, AuthenticationExamplesDbContext>();

            return services;
        }

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        private static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBasicAuthService, BasicAuthService>();

            return services;
        }
    }
}
