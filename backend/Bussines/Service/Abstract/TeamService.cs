using AutoMapper;
using Bussines.DTO;
using Bussines.Service.Concrete;
using DataAccess.Models;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Http;
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
        private readonly IMediaService _mediaService;
        private readonly IMapper _mapper;
        public TeamService(IGenericRepository<Team> repository, IMapper mapper, IMediaService mediaService)
        {
            _repository = repository;
            _mapper = mapper;
            _mediaService = mediaService;   

        }
        public async Task<bool> CreateTeam(TeamDto team,IFormFile file)
        {
            var mapTeam = _mapper.Map<Team>(team);
            var result = await _repository.Add(mapTeam);
            var imageResult = await _mediaService.Storage(file);
            return result;
        }

        public async Task<bool> DeleteTeam(TeamDto teamDto)
        {
            var mapTeam = _mapper.Map<Team>(teamDto);
            var result =  _repository.Delete(mapTeam);
            return result;
        }

        public async Task<List<TeamDto>> GetAllTeam()
        {
            var result =await _repository.GetAll();
            var mapTeam = _mapper.Map<List<TeamDto>>(result);
            return mapTeam;
        }

        public async Task<TeamDto> GetByTeam(int teamId)
        {
            var selectedTeam = await _repository.GetById(teamId);
            var mapDto = _mapper.Map<TeamDto>(selectedTeam);    
            return mapDto;
        }

        public Task<bool> TeamAddUser(List<int> userId)
        {
            throw new NotImplementedException();
            //Kullanıcı birden fazla kişiye istek atıcak kabul edilenler kayıt edilecek.
        }

        public async Task<bool> UpdateTeam(TeamDto teamDto)
        {
            var mapTeam = _mapper.Map<Team>(teamDto);
            var result =  _repository.Update(mapTeam);
            return result;
        }
    }
}
