using BasicAuth.Api.Controllers.Base;
using BasicAuth.Api.Filters.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuth.Api.Controllers
{
    [Route("[controller]")]
    public class ProtectedController : BasicAuthBaseController
    {
        [HttpPost("test")]
        [BasicAuthenticationFilter]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult TestAsync()
        {
            return Ok();
        }
    }
}
