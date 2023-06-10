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
        public string name { get; set; }
        public string description { get; set; }
        public int AdminId { get; set; }
        public UserDto? admin { get; set; }
        public IFormFile File { get; set; }
    }
}
