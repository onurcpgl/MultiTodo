using AutoMapper;
using Bussines.DTO;
using Bussines.Service.Abstract;
using Bussines.Service.Concrete;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System.Drawing.Printing;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        public TeamController(ITeamService teamService,IMapper mapper, IConfiguration config, IMediaService mediaService,IUserService userService)
        {
            _teamService = teamService;
            _userService = userService;
            _mapper = mapper;
            _config = config;

        }
        [HttpPost("/save-team")]
        [Authorize]
        public async Task<ApiResponse> createdTeam([FromBody] TeamDto teamDto)
        {
            var result = await _teamService.CreateTeam(teamDto, HttpContext.User);
            return result;
        }
        [HttpGet("/all-team")]
        [Authorize]
        public async Task<ActionResult<List<TeamDto>>> getAllTeam()
        {
            var result = await _teamService.GetAllTeam(HttpContext.User);
            if (result != null)
            {
                return result;
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet("/team/{id}")]
        [Authorize]
        public async Task<ActionResult<TeamDto>> getByTeamId(int id)
        {
            var result = await _teamService.GetByTeam(id,HttpContext.User);
            return result;
        }

        [HttpDelete("/delete-team/{id}")]
        [Authorize]
        public async Task<ApiResponse> deleteTeam(int id)
        {
            var result = await _teamService.DeleteTeam(id);
            return result;
        }
        [HttpPut("/update-team")]
        [Authorize]
        public async Task<bool> updateTeam([FromForm] TeamDto teamDto)
        {
            var result =await _teamService.UpdateTeam(teamDto);
            return result;
        }

        [HttpPost("/user-invite")]
        [Authorize]
        public async Task<ApiResponse> userInviteTeam([FromBody] RequestDto requestDto)
        {
            var result = await _teamService.UserInvite(requestDto,HttpContext.User);
            return result;
        }
        
        [HttpGet("/team-member/{teamId}")]
       
        public async Task<List<UserDto>> teamMember(int teamId)
        {
            var result = await _teamService.TeamMember(teamId);
            return result;
        }
        [HttpGet("/team-owner/{teamId}")]
        public async Task<UserDto> teamOwner(int teamId)
        {
            var result = await _teamService.TeamOwnerProfile(teamId);
            return result;
        }
    }
}
