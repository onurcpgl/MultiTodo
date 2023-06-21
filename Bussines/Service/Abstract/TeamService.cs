using AutoMapper;
using Bussines.DTO;
using Bussines.Service.Concrete;
using DataAccess.Models;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Abstract
{
    public class TeamService : ITeamService
    {
        private readonly IGenericRepository<Team> _repository;
        private readonly IMediaService _mediaService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public TeamService(IGenericRepository<Team> repository, IMapper mapper, IMediaService mediaService,IUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _mediaService = mediaService;
            _userService = userService;

        }
        public async Task<ApiResponse> CreateTeam(TeamDto team,ClaimsPrincipal claimsPrincipal)
        {
            if(team.formFile != null)
            {
                var userId = claimsPrincipal.FindFirst("userid")?.Value;
                var mediaTeam = await _mediaService.SaveTeamMedia(team.formFile,int.Parse(userId));
                var mapTeam = _mapper.Map<Team>(team);
                mapTeam.media = mediaTeam;
                var ownerUser =  _mapper.Map<User>(_userService.GetByUser(int.Parse(userId)));
                mapTeam.owner = ownerUser;
                mapTeam.ownerId = ownerUser.id;
                var result = await _repository.Add(mapTeam);
                return new ApiResponse { Message = "200", Response = result };
            }
            else
            {
                var userId = claimsPrincipal.FindFirst("userid")?.Value;
                var mapTeam = _mapper.Map<Team>(team);
                var ownerUser = await _userService.GetByUserModal(int.Parse(userId));
                mapTeam.owner = ownerUser;
                mapTeam.ownerId = int.Parse(userId);
                var result = await _repository.Add(mapTeam);
                return new ApiResponse { Message = "200", Response = result };
            }
            
        }

        public async Task<bool> DeleteTeam(TeamDto teamDto)
        {
            var mapTeam = _mapper.Map<Team>(teamDto);
            var result =  _repository.Delete(mapTeam);
            return result;
        }

        public async Task<List<TeamDto>> GetAllTeam()
        {
            var result = await _repository.GetAllWithInclude(true, x => x.owner).ToListAsync();
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
