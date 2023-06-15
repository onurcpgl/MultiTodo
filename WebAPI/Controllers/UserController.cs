using Bussines.DTO;
using Bussines.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using DataAccess.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IMediaService _mediaService;

        public UserController(IUserService userService, IMediaService mediaService, IAuthService authService)
        {
            _userService = userService;
            _mediaService = mediaService;   
            _authService = authService;
        }
        [HttpPost("/user")]
        public async Task<ApiResponse> AddUser([FromBody] UserDto userDto)
        {
            var result = await _userService.SaveUser(userDto);
            return result;
        }

        [HttpGet("/user-profile")]
        [Authorize]
        public async Task<UserDto> GetUser()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.FindFirst("userid")?.Value;
            var result = await _userService.GetByUser(int.Parse(userId));
            return result;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userLoginDto)
        {
            
                var user = await _authService.Authenticate(userLoginDto);
                if(user == null)
                {
                    return BadRequest("Kullanıcı adı veya şifre yanlış");
                }
                else
                {
                    var token = _authService.Generate(user);
                    _authService.UpdateRefreshToken(token.Result.RefreshToken,user ,token.Result.Expiration);
                    return Ok(token);
                }
            
        }
        [HttpPut("/user-update")]
        [Authorize]
        public async Task<bool> UpdateUser([FromBody] UserDto userDto,IFormFile? formFile)
        {
            var currentUser = HttpContext.User;

            var userId = currentUser.FindFirst("userid")?.Value;

            var user = await _userService.GetByUser(int.Parse(userId));

            if (formFile != null)
            {
                MediaDto media = new MediaDto
                {
                    RealFilename = formFile.FileName,
                    FilePath   = formFile.FileName, 
                    RootPath = formFile.FileName,   
                    ServePath = formFile.FileName,
                    AbsolutePath = formFile.FileName,   
                    Mime   = formFile.FileName,
                    userId = user.id,
                    user = userDto
                };
                
            }
            var result =await _userService.UserUpdate(userDto);
            return result;
        }
        [HttpGet("/all-user")]
        public async Task<List<UserDto>> GetAllUser()
        {
            var result = await _userService.GetAllUser();
            return result;
        }

    }
}
