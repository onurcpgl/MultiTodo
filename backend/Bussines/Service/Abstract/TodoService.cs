using AutoMapper;
using Bussines.DTO;
using Bussines.Service.Concrete;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
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
            var todoList = (List<Todo>)result;
            todoList.Sort((x, y) => y.id.CompareTo(x.id)); 
            return todoList;
        }

        public async Task<Todo> GetByIdTodo(int id)
        {
            var result = await _repository.GetById(id);
            return result;
        }

        public async Task<List<Todo>> GetUserTodos(int id)
        {
            var result =await  _repository.GetWhere(x => x.Userid == id).ToListAsync();
            var todoList = (List<Todo>)result;
            todoList.Sort((x, y) => y.id.CompareTo(x.id));
            return todoList;
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

        public async Task<bool> SaveTodo(Todo todo)
        {
            var todoResult = await _repository.Add(todo);
            
            if (!todoResult)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<Todo> SaveTodom(Todo todo)
        {
            var todoResult = await _repository.AddModel(todo);
            return todoResult;
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
