using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers.Enums
{
    public enum TaskResultEnum
    {
        None = 0,
        [Description("Yapıldı")]
        Completed = 1,
        [Description("Yapılmadı")]
        NotCompleted = 2
    }
}
