using Bussines.DTO;
using Bussines.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        [HttpGet("/all-todo")]
        [Authorize]
        public async Task<List<Todo>> allTodo()
        {
            var result = await _todoService.GetAllTodos();
            return result;

        }
        [HttpPost("/save-todo")]
        public async Task<IActionResult> addTodo([FromBody] TodoDto todoDto)
        {
            var result = await _todoService.SaveTodo(todoDto);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok(true);
            }
        }
        [HttpGet("/get-todo")]
        public async Task<Todo> getByTodo(int id)
        {
            var result = await _todoService.GetByIdTodo(id);
            return result;
        }
        [HttpPut("/todo-update")]
        public async Task<bool> todoUpdate([FromBody] Todo todo)
        {
            var result = await _todoService.UpdatedTodo(todo);
            if(!result)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }
        [HttpDelete("delete-todo")]
        public async Task<bool> deleteTodo(int id)
        {
            var result = await _todoService.DeleteTodo(id);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
