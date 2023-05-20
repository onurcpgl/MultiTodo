using Bussines.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Concrete
{
    public interface ITeamService
    {
        Task<bool> CreateTeam(TeamDto team,IFormFile form);
        Task<List<TeamDto>> GetAllTeam();
        Task<TeamDto> GetByTeam(int teamId);
        Task<bool> DeleteTeam(TeamDto teamDto);
        Task<bool> UpdateTeam(TeamDto teamDto);
        Task<bool> TeamAddUser(List<int> userId);
    }
}
