using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Registration()
        {
            return Ok("Hello There");
        }

        [HttpPost("login")]
       public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}
