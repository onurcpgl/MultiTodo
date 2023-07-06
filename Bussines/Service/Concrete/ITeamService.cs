using Bussines.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Models.Models;
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
        Task<TeamDto> GetByTeam(int teamId,ClaimsPrincipal claimsPrincipal);
        Task<List<UserDto>> TeamMember(int teamId);
        Task<ApiResponse> DeleteTeam(int id);
        Task<ApiResponse> UserInvite(RequestDto requestDto, ClaimsPrincipal claimsPrincipal);
        Task<bool> UpdateTeam(TeamDto teamDto);
        Task<bool> TeamAddUser(List<int> userId);
        Task<UserDto> TeamOwnerProfile(int teamId);
        Task<bool> IsAdmin(int teamId,ClaimsPrincipal claimsPrincipal);

    }
}
