using Mango.Services.AuthApi.Models.Dto;
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
        public async Task<IActionResult> Register([FromBody]RegistrationRequestDto registrationRequestDto)
        {
           var errorMessage = await _authService.Register(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                responseDto.IsSuccess = false;
                responseDto.Message = errorMessage;
                return BadRequest(responseDto);

            }
            return Ok(responseDto);
        }

        [HttpPost("login")]
       public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
        {
            var loginResponse = await _authService.Login(loginRequestDto);
            if (loginResponse.User == null)
            {
                responseDto.IsSuccess=false;
                responseDto.Message = "Password of UserName is Incorrect!";
                return BadRequest(responseDto);
            }
            responseDto.Result = loginResponse;
            return Ok(responseDto);
        }
    }
}
