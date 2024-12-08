using AuthenticationExamples.Application.Auth.BasicAuthentication;
using AuthenticationExamples.Application.Exceptions;
using AuthenticationExamples.Application.Models.Request.BasicAuthentication;
using BasicAuth.Api.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuth.Api.Controllers
{
    [Route("[controller]")]
    public class AccountController(IBasicAuthService basicAuthService) : BasicAuthBaseController
    {
        [HttpPost("register")]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BasicAuthExceptionMessage))]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            await basicAuthService.RegisterAsync(request);

            return Ok();
        }
    }
}
