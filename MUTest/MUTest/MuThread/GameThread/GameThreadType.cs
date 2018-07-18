using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUTest.MuThread.GameThread
{
    public enum GameThreadType
    {
        UnKnow = 0,
        WorldThread = 1,
        LogicThread = 2,
        LoginThread = 3,
        CreateRoleThread = 4,
        MapThread = 5,
        FriendThread = 6,
        RedPacketThread = 7,
        GodThreasure = 8,
    }
}
