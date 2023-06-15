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
        public async Task<JWTToken> RefreshTokenLogin([FromBody] string refreshToken)
        {   
            var result = await _authService.RefreshTokenLogin(refreshToken);
            return result;
        }
    }
}
