using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.DTO
{
    public class TodoDto
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int? Userid { get; set; }
    }
}
