using Bussines.DTO;
using Bussines.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Security.AccessControl;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IUserService _userService;
        public TodoController(ITodoService todoService, IUserService userService)
        {
            _todoService = todoService;
            _userService = userService;
        }
        [HttpGet("/get-user-todos")]
        [Authorize]
        public async Task<List<Todo>> userAllTodos()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.FindFirst("userid")?.Value;
            var result = await _todoService.GetUserTodos(int.Parse(userId));
            return result;
        }
        [HttpGet("/all-todo")]
        [Authorize]
        public async Task<List<Todo>> allTodo()
        {
            var result = await _todoService.GetAllTodos();
            return result;

        }
        [HttpPost("/save-todo")]
        [Authorize]
        public async Task<IActionResult> addTodo([FromBody] TodoDto todoDto)
        {

            var currentUser = HttpContext.User;

            var userId = currentUser.FindFirst("userid")?.Value;

            var user = await _userService.GetByUser(int.Parse(userId));


            var newTodo = new Todo
            {
                description = todoDto.description,
                Userid = user.id,
                User = user,
                createdDate = DateTime.UtcNow,
                title = todoDto.title
                
            };

            var result = await _todoService.SaveTodo(newTodo);
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
