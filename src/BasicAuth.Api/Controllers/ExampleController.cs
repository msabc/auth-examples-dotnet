using AuthenticationExamples.Application.Services.Example;
using BasicAuth.Api.Controllers.Base;
using BasicAuth.Api.Filters.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuth.Api.Controllers
{
    [Route("[controller]")]
    public class ExampleController(IExampleService exampleService) : BasicAuthBaseController
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = "BasicAuthentication")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult TestAsync()
        {
            return Ok(exampleService.ExampleGet());
        }
    }
}
