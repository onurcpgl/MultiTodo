using Bussines.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Concrete
{
    public interface ITeamService
    {
        Task<ApiResponse> CreateTeam(TeamDto team,ClaimsPrincipal claimsPrincipal);
        Task<List<TeamDto>> GetAllTeam(ClaimsPrincipal claimsPrincipal);
        Task<TeamDto> GetByTeam(int teamId);
        Task<ApiResponse> DeleteTeam(int id);
        Task<bool> UpdateTeam(TeamDto teamDto,ClaimsPrincipal claimsPrincipal);
        Task<bool> TeamAddUser(List<int> userId);
    }
}
