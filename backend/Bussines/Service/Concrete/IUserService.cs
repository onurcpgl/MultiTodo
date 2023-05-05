using Bussines.DTO;
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
        Task<List<User>> GetUser(int id);
        Task<User> GetByUser(int id);
        Task<bool> SaveUser(UserDto user);
        Task<JWTToken> Generate(User user);
        Task<User> Authenticate(UserLoginDto userLoginDto);
        Task<bool> UserUpdate(User user);
    }
}
