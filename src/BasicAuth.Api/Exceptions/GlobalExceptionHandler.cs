using AuthenticationExamples.Application.Exceptions;
using AuthenticationExamples.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BasicAuth.Api.Exceptions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError($"An unexpected error has occurred: {exception.Message}");

            var errorObject = new BasicAuthExceptionMessage()
            {
                ErrorMessage = "An unexpected error occurred. Please try again later."
            };

            if (exception is CustomHttpException customHttpException)
            {
                httpContext.Response.StatusCode = (int)customHttpException.StatusCode;
                await httpContext.Response.WriteAsJsonAsync(errorObject, cancellationToken);
            }
            else
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(errorObject, cancellationToken);
            }

            return true;
        }
    }
}
