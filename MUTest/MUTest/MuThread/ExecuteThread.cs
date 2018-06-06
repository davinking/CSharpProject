﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MUTest.MuThread
{
   /// <summary>
    /// 工作线程：对Thread 和 TCPCmdTask的封装
    /// </summary>
    public class ExecuteThread
    {
        private Thread _Thread = null;

        private Queue<TCPCmdTask> _TCPCmdTaskQueue = new Queue<TCPCmdTask>();

        public ExecuteThread(int threadID)
        {
            _Thread = new Thread(ThreadPoc);
            _Thread.Start(this);
        }

        // public delegate void ThreadStart();
        // ParameterizedThreadStart

        // [ComVisible(false)]
        // public delegate void ParameterizedThreadStart(object obj);


        /// <summary>
        /// 这是一个ParameterizedThreadStart类型的委托： ThreadPoc 被定义为工作线程的入口
        /// </summary>
        public static void ThreadPoc(Object obj)
        {
            var thread = obj as ExecuteThread;

            thread.Run();
        }

        void Run()
        {
            while (true)
            {
                long startTick = DateTime.Now.Ticks;
                long endTick = startTick + 50 * TimeSpan.TicksPerMillisecond;

                //ProcessTask 处理数据
                {
                    Console.WriteLine("Thread Id={0} start ...", Thread.CurrentThread.ManagedThreadId);
                }

                while (DateTime.Now.Ticks < endTick)
                {
                    Thread.Sleep(1);
                }
            }
        }

    }
}
