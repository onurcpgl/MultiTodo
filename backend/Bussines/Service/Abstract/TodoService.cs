using AutoMapper;
using Bussines.DTO;
using Bussines.Service.Concrete;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Abstract
{
    public class TodoService : ITodoService
    {
        private readonly IGenericRepository<Todo> _repository;
        private readonly IMapper _mapper;
        public TodoService(IGenericRepository<Todo> repository, IMapper mapper)
        {
            _repository = repository; 
            _mapper = mapper;
        }

        public async Task<bool> DeleteTodo(int id)
        {
            var findTodo = await _repository.GetById(id);
            var result =  _repository.Delete(findTodo);
            if (!result)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            var result = await _repository.GetAll();
            return (List<Todo>)result;
        }

        public async Task<Todo> GetByIdTodo(int id)
        {
            var result = await _repository.GetById(id);
            return result;
        }

        public async Task<bool> SaveTodo(TodoDto todo)
        {
            var result = _mapper.Map<Todo>(todo);
            var todoResult = await _repository.Add(result);
            if (!todoResult)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> UpdatedTodo(Todo todo)
        {
            var todoMap = _mapper.Map<Todo>(todo);
            var findTodo =  _repository.Update(todoMap);
            if (!findTodo)
            {
                return false;
            }
            else
            {
                return true;
            }
           
        }
    }
}
