using Bussines.DTO;
using Bussines.Service.Concrete;
using DataAccess.Models;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Abstract
{
    public class AuthService : IAuthService
    {   
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthService(IUserService userService, IConfiguration configuration, IGenericRepository<User> genericRepository)
        {
            _userService = userService;
            _configuration = configuration;
            _genericRepository = genericRepository;

        }

        public string CreateRefreshToken()
        {
            byte[] data = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(data);
            return Convert.ToBase64String(data);
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
                expires: DateTime.UtcNow.AddSeconds(10),
                signingCredentials: creds
            );

            JwtSecurityTokenHandler tokenHandler = new();
            jwtToken.AccessToken = tokenHandler.WriteToken(securityToken);
            jwtToken.RefreshToken = CreateRefreshToken();
            jwtToken.Expiration = DateTime.UtcNow.AddSeconds(10);
            return jwtToken;
        }

        public bool IsTokenExpired(ClaimsPrincipal claimsPrincipal)
        {
            var expirationClaim = claimsPrincipal.FindFirst("exp");

            if (expirationClaim != null && !string.IsNullOrEmpty(expirationClaim.Value))
            {
                var expirationUnixTimestamp = long.Parse(expirationClaim.Value);
                var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationUnixTimestamp).UtcDateTime;
                if (expirationDateTime < DateTime.UtcNow)
                {
                    // Token süresi geçmiş
                    return true;
                }
            }

            return false;
        }

        public async Task<ApiResponse> RefreshTokenLogin(string refreshToken)
        {
            var user = await _userService.FindUserWithRefreshToken(refreshToken);
            if (user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                var token = await Generate(user);
                await UpdateRefreshToken(token.RefreshToken, user, token.Expiration);
                return new ApiResponse { Message = "200", Response = token };
            }
            else
                return new ApiResponse { Message = "400", Response = "User not valid" };

        }
        public async Task<User> Authenticate(UserLoginDto userLoginDto)
        {

            var currentUser = await _genericRepository.GetWhereWithInclude(x => x.mail == userLoginDto.mail && x.password == userLoginDto.password, true).FirstOrDefaultAsync();
            Console.WriteLine(currentUser);
            if (currentUser == null)
            {
                return null;
            }
            else
            {
                return currentUser;
            }
        }
        public async Task<bool> UpdateRefreshToken(string refreshToken, User user, DateTime accesTokenTime)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accesTokenTime.AddSeconds(15);
                _genericRepository.Update(user);
                return true;
            }
            else
                return false;

        }
    }
}
