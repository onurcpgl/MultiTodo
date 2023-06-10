using Bussines.DTO;
using DataAccess.Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Concrete
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUser();
        Task<UserDto> GetByUser(int id);
        Task<ApiResponse> SaveUser(UserDto user);
        Task<JWTToken> Generate(User user);
        Task<User> Authenticate(UserLoginDto userLoginDto);
        Task<bool> UserUpdate(UserDto userDto);
    }
}
