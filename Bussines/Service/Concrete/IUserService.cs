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
        Task<User> GetByUserModal(int id);
        Task<ApiResponse> SaveUser(RegisterUserDto user);
        UserDto FindLoginUser(ClaimsPrincipal claimsPrincipal);

        Task<ApiResponse> CheckNotify(ClaimsPrincipal claimsPrincipal);
        Task<bool> UserUpdate(UserDto userDto,ClaimsPrincipal claimsPrincipal);
       
        Task<User> FindUserWithRefreshToken(string refreshToken);
        Task<ApiResponse> NotifyRequestHandler(RequestDto requestDto);
    }
}
