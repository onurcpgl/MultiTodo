using Bussines.DTO;
using Bussines.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("/user")]
        public async Task<bool> AddUser([FromBody] UserDto userDto)
        {
            var result = await _userService.SaveUser(userDto);
            return result;
        }

        [HttpGet("/user-profile/{id}")]
        
        public async Task<UserDto> GetUser(int id)
        {
            var result = await _userService.GetByUser(id);
            return result;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userLoginDto)
        {
            
                var user = await _userService.Authenticate(userLoginDto);
                if(user == null)
                {
                    return BadRequest("Kullanıcı adı veya şifre yanlış");
                }
                else
                {
                    var token = _userService.Generate(user);
                    return Ok(token);
                }
            
        }
        [HttpPut("/user-update")]
        [Authorize]
        public async Task<bool> UpdateUser([FromBody] UserDto userDto)
        {
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
