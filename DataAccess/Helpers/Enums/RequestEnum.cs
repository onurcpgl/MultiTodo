using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers.Enums
{
    public enum RequestEnum
    {
        [Description("Takım İsteği")]
        TeamRequest = 1,
        [Description("Arkadaşlık İsteği")]
        FriendShipRequest = 2,
    }
}
