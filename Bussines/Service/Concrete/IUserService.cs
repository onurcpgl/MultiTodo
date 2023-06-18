using Bussines.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Concrete
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUser();
        Task<UserDto> GetByUser(int id);
        Task<ApiResponse> SaveUser(UserDto user);
        UserDto FindLoginUser(ClaimsPrincipal claimsPrincipal);
        
 
        Task<bool> UserUpdate(UserDto userDto,ClaimsPrincipal claimsPrincipal);
       
        Task<User> FindUserWithRefreshToken(string refreshToken);
    }
}
