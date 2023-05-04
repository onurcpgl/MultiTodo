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
        public TeamController(ITeamService teamService,IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
        }
        [HttpPost("/save-team")]
        [Authorize]
        public async Task<ActionResult<bool>> createdTeam([FromBody] TeamDto teamDto)
        {
            var currentUser = HttpContext.User;

            var userId = currentUser.FindFirst("userid")?.Value;
            var newTeam = new Team
            {
                name = teamDto.name,
                description = teamDto.description,
                AdminId = int.Parse(userId),
            };
            var result = await _teamService.CreateTeam(newTeam);
     
            return result;
        }
        [HttpGet("/all-team")]
        [Authorize]
        public async Task<ActionResult<List<Team>>> getAllTeam()
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
        public async Task<ActionResult<Team>> getByTeamId(int id)
        {
            var result = await _teamService.GetByTeam(id);
            if (result is Team team)
            {
                return team;
            }
            else
            {
                return NotFound();
            }
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
