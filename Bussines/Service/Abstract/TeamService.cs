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
        private readonly IGenericRepository<Request> _repositoryRequest;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMediaService _mediaService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public TeamService(IGenericRepository<User> userRepository, IGenericRepository<Team> repository, IGenericRepository<Request> repositoryRequest, IMapper mapper, IMediaService mediaService,IUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _mediaService = mediaService;
            _userService = userService;
            _repositoryRequest = repositoryRequest;
            _userRepository = userRepository;
        }
        public async Task<ApiResponse> CreateTeam(TeamDto team,ClaimsPrincipal claimsPrincipal)
        {
            if(team.formFile != null)
            {
                var userId = claimsPrincipal.FindFirst("userid")?.Value;
                var mediaTeam = await _mediaService.SaveTeamMedia(team.formFile,int.Parse(userId));
                var mapTeam = _mapper.Map<Team>(team);
                mapTeam.media = mediaTeam;
                mapTeam.teamImage = mediaTeam.AbsolutePath + mediaTeam.RealFilename;
                var ownerUser =  _mapper.Map<User>(_userService.GetByUser(int.Parse(userId)));
                mapTeam.owner = ownerUser;
                mapTeam.ownerId = ownerUser.id;
                var result = await _repository.Add(mapTeam);
                if(!result)
                    return new ApiResponse { Message = "Zaten bir takıma sahipsiniz, birden fazla takım kuramazsınız.", Response = result };
                else
                return new ApiResponse { Message = "Takım başarıyla oluşturuldu.", Response = result };
            }
            else
            {
                var userId = claimsPrincipal.FindFirst("userid")?.Value;
                var haveUserTeam = _repository.GetWhere(x => x.ownerId == int.Parse(userId)).FirstOrDefault();
                if(haveUserTeam == null)
                {
                    var mapTeam = _mapper.Map<Team>(team);
                    var ownerUser = await _userService.GetByUserModal(int.Parse(userId));
                    mapTeam.owner = ownerUser;
                    mapTeam.ownerId = int.Parse(userId);
                    var result = await _repository.Add(mapTeam);
                    return new ApiResponse { Message = "Takım başarıyla oluşturuldu.", Response = result };
                }
                else
                {
                    return new ApiResponse { Message = "Zaten bir takıma sahipsiniz, birden fazla takım kuramazsınız.", Response = false };
                }     
            }
            
        }

        public async Task<ApiResponse> DeleteTeam(int id)
        {
            var findTeam = await _repository.GetById(id);
            var result = _repository.Delete(findTeam);
            if(result)
                return new ApiResponse { Message = "Silme işlemi başarılı", Response = 200 };
            else 
                return new ApiResponse { Message = "Silme işlemi gerçekleşmedi.",Response = 400 };
        }

        public async Task<List<TeamDto>> GetAllTeam(ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst("userid")?.Value;
            var result = await _repository.GetAllWithInclude(true, x => x.owner).ToListAsync();
            
            foreach (var item in result)
            {
                if (item.ownerId != int.Parse(userId))
                {
                    item.owner = null;
                }
                
            }
            var mapTeam = _mapper.Map<List<TeamDto>>(result);
            return mapTeam;
        }

        public async Task<TeamDto> GetByTeam(int teamId, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst("userid").Value;

            var selectedTeam = await _repository.GetById(teamId);
            if (selectedTeam.ownerId == int.Parse(userId))
            {
                var mapDto = _mapper.Map<TeamDto>(selectedTeam);
                return mapDto;
            }
            else
                return null;
           
        }

        public async Task<bool> IsAdmin(int teamId, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst("userId").Value;
            var team =  _repository.GetWhereWithInclude(x => x.id == teamId,true).FirstOrDefault();
            if (team.ownerId == int.Parse(userId))
                return true;
            else
                return false;

        }

        public Task<bool> TeamAddUser(List<int> userId)
        {
            throw new NotImplementedException();
            //Kullanıcı birden fazla kişiye istek atıcak kabul edilenler kayıt edilecek.
        }

        public async Task<List<UserDto>> TeamMember(int teamId)
        {

            var teamMember = await _userRepository
            .GetAllWithInclude(true, x => x.Teams, x => x.media)
            .Where(x => x.Teams.Any(t => t.id == teamId))
            .ToListAsync();

            foreach (var member in teamMember)
            {
                if (member.media != null)
                {
                    member.teamImage = $"http://localhost:9000/{member.media.FilePath}/{member.media.RealFilename}";

                }
            }
            var mapTeamMember = _mapper.Map<List<UserDto>>(teamMember);
            return mapTeamMember;

          

        }

        public async Task<UserDto> TeamOwnerProfile(int teamId)
        {
            var result = await _repository.GetWhereWithInclude(x => x.id == teamId, true, x => x.owner.media).FirstOrDefaultAsync();
            var mapUser = _mapper.Map<UserDto>(result.owner);
            if (result.owner.media != null)
                mapUser.teamImage = $"http://localhost:9000/{result.owner.media.FilePath}/{result.owner.media.RealFilename}";
            return mapUser;
        }

        public async Task<bool> UpdateTeam(TeamDto teamDto)
        {
            if (teamDto.formFile != null)
            {
                var teamMedia = await _mediaService.SaveTeamMedia(teamDto.formFile, teamDto.id);
                var mapTeam = _mapper.Map<Team>(teamDto);
                mapTeam.media = teamMedia;
                mapTeam.teamImage = $"http://localhost:9000/{teamMedia.FilePath}/{teamMedia.RealFilename}";
                var result = _repository.Update(mapTeam);
                return result;
            }
            else
            {
                var mapTeam = _mapper.Map<Team>(teamDto);
                var result = _repository.Update(mapTeam);
                return result;
            }
        }

        public async Task<ApiResponse> UserInvite(RequestDto requestDto, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst("userId").Value;
            var haveTeam =  _repository.GetWhere(x => x.ownerId == int.Parse(userId));
            if(haveTeam == null)
            {
                //Düzelt
                return new ApiResponse { Message = "İsteğiniz gerçkleştirilemedi.", Response = 400 };
            }
            else
            {
                var request = new Request
                {
                    sendUserId = int.Parse(userId),
                    receiveUserId = requestDto.receiveUserId,
                    requestEnum = requestDto.requestEnum,
                    requestResult = requestDto.requestResult
                };
                var beforeRequest = _repositoryRequest.GetWhere(x => x.sendUserId == request.sendUserId && x.receiveUserId == request.receiveUserId).FirstOrDefault();
                if(beforeRequest == null)
                {
                    var response = await _repositoryRequest.Add(request);
                    return new ApiResponse { Message = "İstek başarıyla gönderildi.", Response = 200 };
                }
                else
                {
                    if (beforeRequest.requestResult != 0)
                    {

                        beforeRequest.requestResult = 0;
                        
                        var response = _repositoryRequest.Update(request);
                        return new ApiResponse { Message = "İstek başarıyla gönderildi.", Response = 200 };
                    }
                    else
                    {
                        return new ApiResponse { Message = "Bu kullanıcıya daha önce istek atılmış.", Response = 400 };
                    }
                }
                
                    
            }
        }
    }
}
