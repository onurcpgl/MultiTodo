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
using System.Security.Claims;
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

        public async Task<List<TodoDto>> GetAllTodos()
        {
            var result = await _repository.GetAll();
            var sortedResult = result.ToList();
            sortedResult.Sort((x, y) => y.id.CompareTo(x.id)); 
            var mapTodo = _mapper.Map<List<TodoDto>>(result);
            return mapTodo;
        }

        public async Task<TodoDto> GetByIdTodo(int id)
        {
            var result = await _repository.GetById(id);
            var mapTodo = _mapper.Map<TodoDto>(result);
            return mapTodo;
        }

        public async Task<List<TodoDto>> GetUserTodos(ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst("userid")?.Value;
            var result =await  _repository.GetWhere(x => x.Userid == int.Parse(userId)).ToListAsync();
            result.Sort((x, y) => y.id.CompareTo(x.id));
            var mapTodo = _mapper.Map<List<TodoDto>>(result);   
            return mapTodo;
        }

        public async Task<bool> SaveTodo(TodoDto todoDto)
        {
            var result = _mapper.Map<Todo>(todoDto);
            var todoResult = await _repository.Add(result);
            return todoResult;
        }


        public async Task<bool> UpdatedTodo(TodoDto todoDto)
        {
            var todoMap = _mapper.Map<Todo>(todoDto);
            var result =  _repository.Update(todoMap);
            return result;
           
        }
    }
}
