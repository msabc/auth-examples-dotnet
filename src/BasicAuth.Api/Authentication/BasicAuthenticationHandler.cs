using System.Security.Claims;
using System.Text.Encodings.Web;
using AuthenticationExamples.Application.Services.BasicAuthentication;
using AuthenticationExamples.Domain.Models.Tables;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace BasicAuth.Api.Authentication
{
    public class BasicAuthenticationHandler(
        IBasicAuthenticationService basicAuthenticationService,
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("Authorization", out StringValues value))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            try
            {
                var user = await basicAuthenticationService.AuthenticateAsync(value);

                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim(ClaimTypes.Name, user.Username)
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }
    }

}
