using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MUTest.MuThread.GameThread
{
    public class GameExecuteThread
    {
        private int m_ThreadId;
        private GameThreadType m_ThreadType;
        private Thread m_Thread;
        private Queue<GameTCPCmdTask> m_AddTaskQueue = new Queue<GameTCPCmdTask>();
        private Queue<GameTCPCmdTask> m_DoingTaskQueue = new Queue<GameTCPCmdTask>();
        private object m_QueueMutex = new object();

        /// <summary>
        /// 线程的序号
        /// </summary>
        public int ThreadId
        {
            get;
            private set;
        }

        /// <summary>
        /// 线程的类型
        /// </summary>
        public GameThreadType ThreadType
        {
            get;
            private set;
        }

        /// <summary>
        /// 入队列
        /// </summary>
        /// <param name="task"></param>
        public void AddGameCmdTask(GameTCPCmdTask task)
        {
            if (null == task)
            {
                Console.WriteLine("AddGameCmdTask is Null!");
                return;
            }

            lock (m_QueueMutex)
            {
                m_AddTaskQueue.Enqueue(task);
            }
        }

        public GameTCPCmdTask PopGameCmdTask()
        {
            GameTCPCmdTask restTask = null;
            lock (m_QueueMutex)
            {
                if(m_DoingTaskQueue.Count > 0)
                {
                    restTask = m_DoingTaskQueue.Dequeue();
                    return restTask;
                }

                if(m_AddTaskQueue.Count >0)
                {
                    Queue<GameTCPCmdTask> tmpQueue = m_AddTaskQueue;
                    m_AddTaskQueue = m_DoingTaskQueue;
                    m_DoingTaskQueue = tmpQueue;
                }
            }
            return restTask;
        }
    }
}
