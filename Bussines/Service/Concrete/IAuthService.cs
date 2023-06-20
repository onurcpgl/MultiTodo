using Bussines.DTO;
using DataAccess.Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Concrete
{
    public interface IAuthService
    {
        bool IsTokenExpired(ClaimsPrincipal claimsPrincipal);
        Task<ApiResponse> RefreshTokenLogin(string refreshToken);
        Task<JWTToken> Generate(User user);
        Task<User> Authenticate(UserLoginDto userLoginDto);
        string CreateRefreshToken();
        Task<bool> UpdateRefreshToken(string refreshToken, User user, DateTime accesTokenTime);

    }
}
