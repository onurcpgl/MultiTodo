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
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            var result = await _userService.SaveUser(userDto);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok(true);
            }
        }
        [HttpGet("/user-profile/{id}")]
        [Authorize]
        public async Task<User> GetUser(int id)
        {
            var result = await _userService.GetByUser(id);
            return result;
        }
        [HttpPost("/login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var user = await _userService.Authenticate(userLoginDto);
                if(user == null)
                {
                    return BadRequest("Invalid username or password");
                }
                else
                {
                    var token = _userService.Generate(user);
                    return Ok(token);
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
           
        }
        
    }
}
