using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUTest.MuAttribute
{
    [TCPCmdHandler]
    public class MapManager
    {
        private MapManager() { }

        private static MapManager instance = new MapManager();

        public static MapManager GetInstance()
        {
            if (null == instance)
                instance = new MapManager();
            return instance;
        }

        [TCPCmdMethod(CmdId = 2000, ThreadMode = 10)]
        private static bool ProcessSpriteMapChangesCmd(int client1, int mapCode)
        {
            return true;
        }
    }
}
