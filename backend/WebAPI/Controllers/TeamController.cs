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
        public async Task<ActionResult<bool>> createdTeam([FromForm] TeamDto teamDto)
        {
            var file = teamDto.File;

            var currentUser = HttpContext.User;

            var userId = currentUser.FindFirst("userid")?.Value;
            var adminUser = await _userService.GetByUser(int.Parse(userId));

            var newTeam = new TeamDto
            {
                name = teamDto.name,
                description = teamDto.description,
                admin = adminUser,
                AdminId = int.Parse(userId),
            };
            
            var result = await _teamService.CreateTeam(newTeam,teamDto.File);
     
            return result;
        }
        [HttpGet("/all-team")]
        [Authorize]
        public async Task<ActionResult<List<TeamDto>>> getAllTeam()
        {
            var result = await _teamService.GetAllTeam();
            if (result != null)
            {
                return result;
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet("/getby-team/{id}")]
        [Authorize]
        public async Task<ActionResult<TeamDto>> getByTeamId(int id)
        {
            var result = await _teamService.GetByTeam(id);
            return result;
        }

        [HttpDelete("/delete-team/{id}")]
        [Authorize]
        public async Task<ActionResult> deleteTeam(int id)
        {
            var findTeam = await _teamService.GetByTeam(id);
            var result = await _teamService.DeleteTeam(findTeam);
            if(result)
            {
                return Ok("Silme işlemi gerçekleşti.");
            }
            else
            {
                return BadRequest("Silme işlemi gerçekleştirilemedi.");
            }

        }
        [HttpPut("/update-team/{id}")]
        [Authorize]
        public async Task<ActionResult> updateTeam([FromBody] TeamDto teamDto,int id)
        {
            var checkTeam = await _teamService.GetByTeam(id);
            if (checkTeam == null)
            {
                return NotFound();
            }
            _mapper.Map(teamDto, checkTeam);

            
            var result =await _teamService.UpdateTeam(checkTeam);
            if (result)
            {
                return Ok("Güncelleme işlemi tamamlandı.");
            }
            else
            {
                return BadRequest("Güncelleme tamamlanmadı.");
            }
        }
}
}
