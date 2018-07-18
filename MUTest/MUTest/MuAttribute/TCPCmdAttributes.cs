using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUTest.MuAttribute
{

    /// <summary>
    /// 定义了特性： TCPCmdClassAttribute
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class TCPCmdAttribute : System.Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TCPCmdMethodAttribute : Attribute
    {
        public int CmdId
        {
            get;
            set;
        }

        public int ThreadMode;

        public TCPCmdMethodAttribute() { }
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class TCPCmdHandlerAttribute : Attribute
    {

    }
}
