using AutoMapper;
using Bussines.DTO;
using Bussines.Service.Concrete;
using DataAccess.Models;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Abstract
{

    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
       

        public UserService(IGenericRepository<User> genericRepository, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _configuration = configuration;
        }

        public async Task<List<UserDto>> GetAllUser()
        {
            var result = await _genericRepository.GetAll();
            var mapAllUser = _mapper.Map<List<UserDto>>(result);
            return mapAllUser;
        }

        public async Task<UserDto> GetByUser(int id)
        {
            var result =  _genericRepository.GetWhere(x => x.id == id).FirstOrDefault();
            var userDto = _mapper.Map<UserDto>(result);
            return userDto;
        }

        public async Task<ApiResponse> SaveUser(UserDto userDto)
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

        public async Task<bool> UserUpdate(UserDto userDto)
        {
            var mapUser = _mapper.Map<User>(userDto);
            var result =  _genericRepository.Update(mapUser);
            return result;
        }

      
        public async Task<User> FindUserWithRefreshToken(string refreshToken)
        {
            var result =  _genericRepository.GetWhere(x => x.RefreshToken == refreshToken).FirstOrDefault();
            return result;
        }
    }
}
