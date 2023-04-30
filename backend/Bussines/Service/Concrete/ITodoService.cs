using Bussines.DTO;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Concrete
{
    public interface ITodoService
    {
        Task<List<Todo>> GetAllTodos();
        Task<Todo> GetByIdTodo(int id);
        Task<bool> SaveTodo(TodoDto todo);
        Task<bool> UpdatedTodo(Todo todo);
        Task<bool> DeleteTodo(int id);

    }
}
