using AuthenticationExamples.Application.Auth.BasicAuthentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BasicAuth.Api.Filters.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthenticationFilter : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var basicAuthService = context.HttpContext.RequestServices.GetRequiredService<IBasicAuthService>();

            bool authenticated = await basicAuthService.AuthenticateAsync(context.HttpContext.Request.Headers.Authorization);
         
            if (!authenticated)
                context.Result = new ForbidResult();
        }
    }
}
