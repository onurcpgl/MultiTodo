using AutoMapper;
using Bussines.DTO;
using Bussines.Service.Concrete;
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

        public async Task<User> Authenticate(UserLoginDto userLoginDto)
        {
            
            var currentUser = await _genericRepository.GetWhereWithInclude(x => x.mail == userLoginDto.mail && x.password == userLoginDto.password, true).FirstOrDefaultAsync();
            Console.WriteLine(currentUser);
            if (currentUser == null)
            {
                return null;
            }else
            {
                return currentUser;
            } 
        }

        public async Task<JWTToken> Generate(User user)
        {
            JWTToken jwtToken = new();

            var claims = new List<Claim>();
            claims.Add(new Claim("firstname", user.firstName));
            claims.Add(new Claim("lastname", user.lastName));
            claims.Add(new Claim("userId", user.id.ToString()));
            claims.Add(new Claim("email", user.mail));


            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken securityToken = new(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            JwtSecurityTokenHandler tokenHandler = new();
            jwtToken.AccessToken = tokenHandler.WriteToken(securityToken);
            return jwtToken;
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

        public async Task<bool> SaveUser(UserDto userDto)
        {
            var result = _mapper.Map<User>(userDto);
            var userResult = await _genericRepository.Add(result);
            return userResult;
        }

        public async Task<bool> UserUpdate(UserDto userDto)
        {
            var mapUser = _mapper.Map<User>(userDto);
            var result =  _genericRepository.Update(mapUser);
            return result;
        }
    }
}
