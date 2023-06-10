using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Todo
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime createdDate { get; set; }
        public int Userid { get; set; }
        public User User { get; set; }

    }
}
