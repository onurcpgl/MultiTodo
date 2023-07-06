using Microsoft.AspNetCore.Http;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Team
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int? ownerId { get; set; }
        public string? teamImage { get; set; }
        public User? owner { get; set; }  
        public ICollection<User>? memberList { get; set; }
        public Media? media { get; set; }
        public bool status { get; set; }
        public ICollection<TeamTask>? TeamTasks { get; set; } 

    }
}
