using DataAccess.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.DTO
{
    public class NotifyDto
    {
        public UserDto SendUser { get; set; }
        public RequestEnum RequestEnum { get; set; }    
        
    }
}
