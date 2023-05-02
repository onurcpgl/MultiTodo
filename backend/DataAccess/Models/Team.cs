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
        public string name { get; set; }
        public string description { get; set; }
        public int AdminId { get; set; }
        public User AdminUser { get; set; }
        public ICollection<User> memberList { get; set; }

    }
}
