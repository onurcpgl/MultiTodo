using AutoMapper;
using Bussines.DTO;
using Bussines.Service.Concrete;
using DataAccess.Models;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Abstract
{
    public class TeamService : ITeamService
    {
        private readonly IGenericRepository<Team> _repository;
        private readonly IMapper _mapper;
        public TeamService(IGenericRepository<Team> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> CreateTeam(Team team)
        {
            var result = await _repository.Add(team);
            return result;
        }

        public async Task<bool> DeleteTeam(Team team)
        {
            var result =  _repository.Delete(team);
            return result;
        }

        public async Task<List<Team>> GetAllTeam()
        {
            var result =await _repository.GetAll();
            return (List<Team>)result;
        }

        public async Task<Team> GetByTeam(int teamId)
        {
            var selectedTeam = await _repository.GetById(teamId);
            return selectedTeam;
        }

        public Task<bool> TeamAddUser(List<int> userId)
        {
            throw new NotImplementedException();
            //Kullanıcı birden fazla kişiye istek atıcak kabul edilenler kayıt edilecek.
        }

        public async Task<bool> UpdateTeam(Team team)
        {
            var result =  _repository.Update(team);
            return result;
        }
    }
}
