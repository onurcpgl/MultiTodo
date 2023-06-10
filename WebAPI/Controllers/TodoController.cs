﻿using Bussines.DTO;
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
    [Route("[controller]")]
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
        public bool IsTokenExpired(Claim expirationClaim)
        {
            if (expirationClaim != null && !string.IsNullOrEmpty(expirationClaim.Value))
            {
                var expirationUnixTimestamp = long.Parse(expirationClaim.Value);
                var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationUnixTimestamp).UtcDateTime;
                if (expirationDateTime < DateTime.UtcNow)
                {
                    // Token süresi geçmiş
                    return true;
                }
            }

            return false;
        }
        [HttpGet("/get-user-todos")]
        [Authorize]
        public async Task<List<TodoDto>> UserAllTodos()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.FindFirst("userid")?.Value;
            var expirationClaim = currentUser.FindFirst("exp");
            // Tokenın süresini kontrol etme
            if (IsTokenExpired(expirationClaim))
            {
                // Token süresi geçmiş, hata yanıtı dönme
                Response.StatusCode = 401; // Unauthorized
                return null;
            }


            var result = await _todoService.GetUserTodos(int.Parse(userId));
            return result;
        }
        [HttpGet("/all-todo")]
        [Authorize]
        public async Task<List<TodoDto>> allTodo()
        {
            var result = await _todoService.GetAllTodos();
            return result;
        }
        [HttpPost("/save-todo")]
        [Authorize]
        public async Task<bool> addTodo([FromBody] TodoDto todoDto)
        {

            var currentUser = HttpContext.User;

            var userId = currentUser.FindFirst("userid")?.Value;

            var user = await _userService.GetByUser(int.Parse(userId));

     
            var newTodo = new TodoDto
            {
                description = todoDto.description,
                Userid = int.Parse(userId),
                title = todoDto.title

            };

            var result = await _todoService.SaveTodo(newTodo);
            return result;
        }
        [HttpGet("/get-todo")]
        [Authorize]
        public async Task<TodoDto> getByTodo(int id)
        {
            var result = await _todoService.GetByIdTodo(id);
            return result;
        }
        [HttpPut("/todo-update")]
        [Authorize]
        public async Task<bool> todoUpdate([FromBody] TodoDto todo)
        {
            var result = await _todoService.UpdatedTodo(todo);
            return result;

        }
        [HttpDelete("/delete-todo/{id}")]
        [Authorize]
        public async Task<bool> deleteTodo(int id)
        {
            var result = await _todoService.DeleteTodo(id);
            return result;

        }
    }
}
