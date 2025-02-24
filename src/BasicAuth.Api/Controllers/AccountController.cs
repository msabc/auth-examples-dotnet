using AuthenticationExamples.Application.Exceptions;
using AuthenticationExamples.Application.Models.Request.BasicAuthentication;
using AuthenticationExamples.Application.Services.Users;
using BasicAuth.Api.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuth.Api.Controllers
{
    [Route("[controller]")]
    public class AccountController(IUserService userService) : BasicAuthBaseController
    {
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BasicAuthExceptionMessage))]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            await userService.RegisterAsync(request);

            return Ok();
        }
    }
}
