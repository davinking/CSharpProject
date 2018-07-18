using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUTest.MuAttribute
{

    /// <summary>
    /// 被自定义的特性装饰
    /// </summary>
    [TCPCmd]
    public partial class TCPCmdHandler
    {
        public static TCPCmdHandler Instance = new TCPCmdHandler();

        public static string KeyData = "12345";

        public void ProcessCmd(int cmdId, byte[] data,int count)
        {

        }

        public bool DispatchCmdToThread(byte[] data, int strart, int count, int threadID = 0)
        {
            return true;
        }

        public bool DispatchCmdToThread<T>(T data, int threadID = 0)
        {
            return true;
        }

        public bool DispatchCmdToThread(int cmdID, string data, int threadID =0)
        {
            return true;
        }
    }
}
