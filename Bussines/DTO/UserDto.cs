using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.DTO
{
    public class UserDto
    {
        public int? id { get; set; } 
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mail { get; set; }
        public IFormFile? formFile { get; set; }
  
    }
}
