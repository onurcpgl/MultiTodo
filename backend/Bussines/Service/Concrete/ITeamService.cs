using Bussines.DTO;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Concrete
{
    public interface ITeamService
    {
        Task<bool> CreateTeam(Team team);
        Task<List<Team>> GetAllTeam();
        Task<Team> GetByTeam(int teamId);
        Task<bool> DeleteTeam(Team team);
        Task<bool> UpdateTeam(Team team);
        Task<bool> TeamAddUser(List<int> userId);
    }
}
