using AutoMapper;
using Bussines.DTO;
using Bussines.Service.Concrete;
using DataAccess.Helpers.Enums;
using DataAccess.Models;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Abstract
{

    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IGenericRepository<Request> _requestRepository;
        private readonly IGenericRepository<Team> _teamRepository;
        private readonly IConfiguration _configuration;
        private readonly IMediaService _mediaService;

        private readonly IMapper _mapper;
       

        public UserService(IGenericRepository<User> genericRepository, IGenericRepository<Team> teamRepository, IGenericRepository<Request> requestRepository, IMapper mapper, IConfiguration configuration,IMediaService mediaService)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _configuration = configuration;
            _mediaService = mediaService;
            _requestRepository = requestRepository;
            _teamRepository = teamRepository;   
        }

        public async Task<List<UserDto>> GetAllUser()
        {
            var result = await _genericRepository.GetAll();
            var mapAllUser = _mapper.Map<List<UserDto>>(result);
            return mapAllUser;
        }

        public async Task<UserDto> GetByUser(int id)
        {
            var result = _genericRepository.GetWhereWithInclude(x => x.id == id ,true,x => x.media).FirstOrDefaultAsync();
            var userDto = _mapper.Map<UserDto>(result);
            return userDto;
        }

        public async Task<ApiResponse> SaveUser(RegisterUserDto userDto)
        {
            //Email adresi ile daha önce kayıt yapılmış mı?

            var checkEmail = _genericRepository.GetWhere(x => x.mail == userDto.mail).FirstOrDefault();
            if(checkEmail != null)
            {
                return new ApiResponse { Response = 409, Message = "Mail adresi ile daha önce kayıt yapılmış." };
            }
            else
            {
                var result = _mapper.Map<User>(userDto);
                var userResult = await _genericRepository.Add(result);
                if(!userResult)
                {
                    return new ApiResponse { Response = 400, Message = "Kayıt işlemi yapılırken bir hata meydana geldi, lütfen daha sonra tekrar deneyiniz." };
                }
                return new ApiResponse { Response = 200, Message = "Kayıt işlemi başarılı." };
            }
            
            
        }
        public async Task<bool> UserUpdate(UserDto userDto,ClaimsPrincipal claimsPrincipal)
        {
          
            if(userDto.formFile != null)
            {
                var userId = claimsPrincipal.FindFirst("userid")?.Value;
                var userMedia = await _mediaService.SaveUserMedia(userDto.formFile,int.Parse(userId));
                var user = _genericRepository.GetById(int.Parse(userId)).Result;
                user.firstName = userDto.firstName;
                user.lastName = userDto.lastName;
                user.mail = userDto.mail;
                user.media = userMedia;
                var result = _genericRepository.Update(user);
                return result;
            }
            else
            {
                var mapUser = _mapper.Map<User>(userDto);
                var result = _genericRepository.Update(mapUser);
                return result;
            }
           
           
            
        }
        public async Task<User> FindUserWithRefreshToken(string refreshToken)
        {
            var result =  _genericRepository.GetWhere(x => x.RefreshToken == refreshToken).FirstOrDefault();
            return result;
        }

        public UserDto FindLoginUser(ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst("userid")?.Value;
            var result = _genericRepository.GetWhereWithInclude(x => x.id == int.Parse(userId), true, x => x.media).FirstOrDefaultAsync();
            var userDto = _mapper.Map<UserDto>(result.Result);
            if(result.Result.media != null)
                userDto.teamImage = $"http://localhost:9000/{result.Result.media.FilePath}/{result.Result.media.RealFilename}";          
            return userDto;
        }

        public async Task<User> GetByUserModal(int id)
        {
            var user = await _genericRepository.GetById(id);
            return user;
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field != null)
            {
                DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    return attribute.Description;
                }
            }
            return value.ToString();
        }
        public async Task<ApiResponse> CheckNotify(ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst("userid")?.Value;
            var result = await _requestRepository.GetWhere(x => x.receiveUserId == int.Parse(userId) && x.requestResult == 0).ToListAsync();
            var transformedRequests = result.Select(request => new
            {
                request.id,
                request.sendUserId,
                request.receiveUserId,
                requestor = _mapper.Map<UserDto>(_genericRepository.GetById(request.sendUserId).Result),
                requestEnum = GetEnumDescription(request.requestEnum),
                requestResult = GetEnumDescription(request.requestResult)
            }).ToList();
            return new ApiResponse { Message = "Bildirim listesi.", Response = transformedRequests };
        }

        public async Task<ApiResponse> NotifyRequestHandler(RequestDto requestDto)
        {
            
            if(requestDto.requestResult == RequestResponse.Reject)
            {
                var requestMap = _mapper.Map<Request>(requestDto);
                var result = _requestRepository.Update(requestMap);
                if (result)
                    return new ApiResponse { Message = "İstek reddedildi." };
                else
                    return new ApiResponse { Message = "Hata meydana geldi." };
            }
            else
            {
                if(requestDto.requestEnum == RequestEnum.TeamRequest)
                {
                    var user = await _genericRepository.GetById(requestDto.receiveUserId);
                    var findTeam = await _teamRepository.GetWhere(x => x.ownerId == requestDto.sendUserId).FirstAsync();

                    if (findTeam.memberList == null)
                    {
                        findTeam.memberList = new List<User>();
                    }
                    findTeam.memberList.Add(user);

                    var result =  _teamRepository.Update(findTeam);
                    var requstMap = _mapper.Map<Request>(requestDto);
                    await _requestRepository.UpdateModel(requstMap);
                    if (result)
                        
                        return new ApiResponse { Message = "Takıma üye kaydı başarıyla tamamlandı."};
                    else
                        return new ApiResponse { Message = "Takıma üye ekleme başarısız." };
                }
                else
                {
                    return new ApiResponse { Message = "Burada arkadaşlık isteği işlemleri yapılacak." };
                }
            }
        }

        public async Task<List<UserDto>> NotTeamMember(int teamId, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst("userid")?.Value;
            var user = await _genericRepository.GetWhereWithInclude(x => x.id == int.Parse(userId),true, x => x.teamOwner).FirstOrDefaultAsync();
           
            if (user.teamOwner != null && user.teamOwner.id == teamId)
            {
                var result = await _genericRepository.GetWhereWithInclude(x => x.id != int.Parse(userId),true, x => x.Teams,x => x.media).Where(x => !x.Teams.Any(t => t.id == teamId)).ToListAsync();
                foreach (var item in result)
                {
                    if(item.media != null)
                    {
                        item.teamImage = $"http://localhost:9000/{item.media.FilePath}/{item.media.RealFilename}";
                    }
                }
                var teamList = result.ToList();
                var mapAllUser = _mapper.Map<List<UserDto>>(teamList);
                return mapAllUser;
            }
            else {
                return new List<UserDto> { };
            }
           
        }

        public async Task<ApiResponse> ChangePassword(PasswordDto passwordDto, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst("userid")?.Value;
            var user = await _genericRepository.GetById(int.Parse(userId));
            if(user == null)
            {
                return new ApiResponse { Message = "Kullanıcı bulunamadı", Response = false };    
            }
            else
            {
                user.password = passwordDto.password;
                var result = _genericRepository.Update(user);
                if(result == null)
                    return new ApiResponse { Message="Şifre değiştirilirken bir hata meydana geldi.", Response = false };
                else
                    return new ApiResponse { Message="Şifre değiştirme işlemi başarılı.", Response = false };
            }
        }
    }
}
