using Microsoft.AspNetCore.Http;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.DTO
{
    public class TeamDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public UserDto? owner { get; set; }
        public IFormFile? formFile { get; set; } 
    }
}
