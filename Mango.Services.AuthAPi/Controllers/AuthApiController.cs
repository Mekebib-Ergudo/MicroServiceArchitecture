using Mango.Services.AuthApi.Service.IService;
using Mango.Services.AuthAPi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto responseDto;
        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
            responseDto = new ResponseDto();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register()
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
