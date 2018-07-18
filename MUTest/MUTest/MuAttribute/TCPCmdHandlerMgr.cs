using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MUTest.MuAttribute
{
    public class TCPCmdHandlerMgr
    {
        private TCPCmdHandlerMgr() { }

        private static TCPCmdHandlerMgr instance = new TCPCmdHandlerMgr();

        public static TCPCmdHandlerMgr Instance()
        {
            return instance;
        }


        public void RegistTCPCmdType(Object instance)
        {
            if (null == instance) return;

            RegistTCPCmdType(instance.GetType(), instance);
        }

        public void RegistTCPCmdType(Type type)
        {
            RegistTCPCmdType(type, null);
        }

        private void RegistTCPCmdType(Type type, object instance)
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.DeclaredOnly;

            if (null != instance)
                flags |= BindingFlags.Instance;

            //int count = 0;

            var methodInfos = GetRemoteTypeMethods(type, flags);

            foreach( var methodInfo in methodInfos)
            {
                var mattrs = methodInfo.GetCustomAttributes(typeof(TCPCmdMethodAttribute), false) as TCPCmdMethodAttribute[];

                if (mattrs.Length < 1)
                    continue;

                var pinfos = methodInfo.GetParameters();

                foreach (var mattr in mattrs)
                {
                    try
                    {
                        if (mattr.CmdId == 0)
                            continue;

                        int cmdId = mattr.CmdId;

                        int threadMode = mattr.ThreadMode;

                        string msg = string.Format("Methrod name is:{0}, TCPCmdMethodAttribute info->(cmdId={1}, threadMod ={2}.).", methodInfo.Name, cmdId, threadMode);

                        Console.WriteLine("===>"+ msg);
                        //TCPGameServerCmds 和
                        //实际应用在这里构造 TCPCmdMethodInfo 然后将对象 Add 到 Dictionary<TCPGameServerCmds, TCPCmdMethodInfo> 中
                        //然后又 TCPCmdHandler 的DispatchCmdToThread 方法来处理 Dictionary中的内容 分发到不同的线程来处理
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

            }

        }



        protected MethodInfo[] GetRemoteTypeMethods(Type type, BindingFlags flags)
        {
            var methods = new List<MethodInfo>();

            while(null != type)
            {
                MethodInfo[] methodInfos = type.GetMethods(flags);

                methods.AddRange(methodInfos);

                //通过反射获取 自定义的特性 不包含继承的类型
                TCPCmdAttribute[] attrs = (TCPCmdAttribute[])type.GetCustomAttributes(typeof(TCPCmdAttribute),false);

                if (attrs.Length < 0)
                    break;

                type = type.BaseType;
            }

            return methods.ToArray();
        }

        /// <summary>
        /// 自动注册： CmdHandlers
        /// </summary>
        public void AutoRegisterCmdHandlers()
        {
            var assembly = Assembly.GetExecutingAssembly();

            foreach (var assType in assembly.GetExportedTypes())
            {
                if (!assType.IsClass)
                    continue;

                var attributes = assType.GetCustomAttributes(typeof(TCPCmdHandlerAttribute),false);

                Console.WriteLine("Class Name is:" + assType.FullName);


                if (attributes.Length == 0) continue;

                RegistTCPCmdType(assType);
            }
        }


        public void Start()
        {
            RegistTCPCmdType(typeof(TCPCmdHandler));

            AutoRegisterCmdHandlers();
        }
    }
}
