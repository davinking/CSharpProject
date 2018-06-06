﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MUTest.SocketTest
{

    // https://msdn.microsoft.com/zh-cn/library/bb517542(v=vs.110).aspx
    // 创建一个大型缓冲区可以划分并分配给 SocketAsyncEventArgs 对象用在每个套接字 I/O 操作。 这使缓冲区可以轻松地重复使用，可防止堆内存碎片化。
    // 缓冲区管理器： 这个类创建一个可以分割的单个大缓冲区。并分配给SocketAsyncEventArgs对象，以便与每个对象一起使用套接字I/O操作。
    // This class creates a single large buffer which can be divided up 
    // and assigned to SocketAsyncEventArgs objects for use with each 
    // socket I/O operation.  
    // This enables bufffers to be easily reused and guards against 
    // fragmenting heap memory. 
    // 可以使缓冲区可以重复使用并避免破碎的堆内存
    // 
    // The operations exposed on the BufferManager class are not thread safe.
    class BufferManager
    {
        int m_numBytes;                 // the total number of bytes controlled by the buffer pool (由缓冲区池控制的字节总数)
        byte[] m_buffer;                // the underlying byte array maintained by the Buffer Manager(缓冲区池管理器维护的基础Byte数组：即维护的多个SocketAsyncEventArg对象字节)
        Stack<int> m_freeIndexPool;     // Pool of indexes for ther buffer Manager 缓冲区管理器的索引池
        int m_currentIndex;             // m_buffer数组对应的当前的索引 （Current index of the uderlying Byte array）
        int m_bufferSize;               // 由缓冲池控制的字节总数 (The total number of bytes controlled by the buffer pool)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalBytes">The total number of bytes for the buffer pool 缓冲区所能缓冲的字节的总数</param>
        /// <param name="bufferSize">Size of the buffer pool 缓冲区池的大小</param>
        public BufferManager(int totalBytes, int bufferSize)
        {
            m_numBytes = totalBytes;
            m_currentIndex = 0;
            m_bufferSize = bufferSize;
            m_freeIndexPool = new Stack<int>();
        }

        // Allocates buffer space used by the buffer pool 分配被缓冲池使用的缓冲区空间
        // 初始化一个缓冲区池：创建m_numBytes 个byte数组
        public void InitBuffer()
        {
            // create one big large buffer and divide that 
            // out to each SocketAsyncEventArg object
            // 创建一个大缓冲区并将其分配给每个SocketAsyncEventArg对象
            m_buffer = new byte[m_numBytes];
        }

        // Assigns a buffer from the buffer pool to the 
        // specified SocketAsyncEventArgs object
        // 将缓冲区从缓冲池分配给指定的SocketAsyncEventArgs对象
        //
        // <returns>true if the buffer was successfully set, else false</returns>
        public bool SetBuffer(SocketAsyncEventArgs args)
        {

            if (m_freeIndexPool.Count > 0)
            {
                args.SetBuffer(m_buffer, m_freeIndexPool.Pop(), m_bufferSize);
            }
            else
            {
                if ((m_numBytes - m_bufferSize) < m_currentIndex)
                {
                    return false;
                }
                args.SetBuffer(m_buffer, m_currentIndex, m_bufferSize);
                m_currentIndex += m_bufferSize;
            }
            return true;
        }

        // 从SocketAsyncEventArg对象中移除缓冲区。这会将缓冲区释放回缓冲池。
        // Removes the buffer from a SocketAsyncEventArg object.  
        // This frees the buffer back to the buffer pool
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            m_freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }

    }
}
