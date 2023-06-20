using Bussines.DTO;
using Bussines.Service.Concrete;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;   
        }

        [HttpPost("/refresh-token")]
        public async Task<object> RefreshTokenLogin([FromBody] string refreshToken)
        {   
            var result = await _authService.RefreshTokenLogin(refreshToken);
            return result;
        }
        [HttpPost("/login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userLoginDto)
        {

            var user = await _authService.Authenticate(userLoginDto);
            if (user == null)
            {
                return BadRequest("Kullanıcı adı veya şifre yanlış");
            }
            else
            {
                var token = _authService.Generate(user);
                _authService.UpdateRefreshToken(token.Result.RefreshToken, user, token.Result.Expiration);
                return Ok(token);
            }

        }
    }
}
