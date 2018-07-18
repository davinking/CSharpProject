using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUTest.MuAttribute
{
    public partial class TCPCmdHandler
    {
        [TCPCmdMethod(CmdId=1000, ThreadMode = 1)]
        public static int ProcessUserLoginCmd(int nID, byte[] data)
        {
            //DOSomething....

            Console.WriteLine(" Process user login cmd");

            return 1;
        }
    }
}
