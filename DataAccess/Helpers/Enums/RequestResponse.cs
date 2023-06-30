using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers.Enums
{
    public enum RequestResponse
    {
        None = 0,
        [Description("Onaylandı")]
        Acccept = 1,
        [Description("Onaylanmadı")]
        Reject = 2
    }
}
