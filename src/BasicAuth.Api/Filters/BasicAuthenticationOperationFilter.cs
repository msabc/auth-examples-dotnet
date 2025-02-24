using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BasicAuth.Api.Filters
{
    public class BasicAuthenticationOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var requiredPolicies = context.MethodInfo
                .GetCustomAttributes(true)
                .Concat(context.MethodInfo.DeclaringType.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>()
                .Select(attr => attr.AuthenticationSchemes)
                .Where(scheme => !string.IsNullOrWhiteSpace(scheme) && scheme.Contains("Basic"))
                .Distinct();

            if (requiredPolicies.Any())
            {
                operation.Security =
                [
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic auth"
                                },
                                Name = "Basic Authorization",
                                Scheme = "basic",
                                In = ParameterLocation.Header,
                                Type = SecuritySchemeType.Http,
                            },
                            []
                        }
                    }
                ];
            }
        }
    }
}