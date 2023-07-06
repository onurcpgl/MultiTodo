using DataAccess.Helpers.Enums;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class TeamTask
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime createdDate { get; set; }
        public int Userid { get; set; }
        public User User { get; set; }
        public TaskResultEnum taskResultEnum { get; set; }
        public Team TaskOwnerTeam { get; set; }
        public DateTime ValidTaskTime { get; set; }
    }
}
