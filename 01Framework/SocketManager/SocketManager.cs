﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using log4net;

namespace Medicom.PASSPA2CollectService
{
    public class SocketManager
    {

        private int m_maxConnectNum;    //最大连接数
        private int m_revBufferSize;    //最大接收字节数
        BufferManager m_bufferManager;
        const int opsToAlloc = 2;
        Socket listenSocket;            //监听Socket
        SocketEventPool m_pool;
        int m_clientCount;              //连接的客户端数量
        Semaphore m_maxNumberAcceptedClients;

        List<AsyncUserToken> m_clients; //客户端列表

        public static readonly ILog logger = LogManager.GetLogger(typeof(SocketManager));

        #region 定义委托

        /// <summary>
        /// 客户端连接数量变化时触发
        /// </summary>
        /// <param name="num">当前增加客户的个数(用户退出时为负数,增加时为正数,一般为1)</param>
        /// <param name="token">增加用户的信息</param>
        public delegate void OnClientNumberChange(int num, AsyncUserToken token);

        /// <summary>
        /// 接收到客户端的数据
        /// </summary>
        /// <param name="token">客户端</param>
        /// <param name="buff">客户端数据</param>
        public delegate void OnReceiveData(AsyncUserToken token, byte[] buff);

        #endregion

        #region 定义事件
        /// <summary>
        /// 客户端连接数量变化事件
        /// </summary>
        public event OnClientNumberChange ClientNumberChange;

        /// <summary>
        /// 接收到客户端的数据事件
        /// </summary>
        public event OnReceiveData ReceiveClientData;


        #endregion

        #region 定义属性

        /// <summary>
        /// 获取客户端列表
        /// </summary>
        public List<AsyncUserToken> ClientList { get { return m_clients; } }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="numConnections">最大连接数</param>
        /// <param name="receiveBufferSize">缓存区大小</param>
        public SocketManager(int numConnections, int receiveBufferSize)
        {
            m_clientCount = 0;
            m_maxConnectNum = numConnections;
            m_revBufferSize = receiveBufferSize;
            // allocate buffers such that the maximum number of sockets can have one outstanding read and 
            //write posted to the socket simultaneously  
            m_bufferManager = new BufferManager(receiveBufferSize * numConnections * opsToAlloc, receiveBufferSize);

            m_pool = new SocketEventPool(numConnections);
            m_maxNumberAcceptedClients = new Semaphore(numConnections, numConnections);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            // Allocates one large byte buffer which all I/O operations use a piece of.  This gaurds 
            // against memory fragmentation
            m_bufferManager.InitBuffer();
            m_clients = new List<AsyncUserToken>();
            // preallocate pool of SocketAsyncEventArgs objects
            SocketAsyncEventArgs readWriteEventArg;

            for (int i = 0; i < m_maxConnectNum; i++)
            {
                readWriteEventArg = new SocketAsyncEventArgs();
                readWriteEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                readWriteEventArg.UserToken = new AsyncUserToken();

                // assign a byte buffer from the buffer pool to the SocketAsyncEventArg object
                m_bufferManager.SetBuffer(readWriteEventArg);
                // add SocketAsyncEventArg to the pool
                m_pool.Push(readWriteEventArg);
            }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="localEndPoint"></param>
        public bool Start(IPEndPoint localEndPoint)
        {
            try
            {
                m_clients.Clear();
                listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listenSocket.Bind(localEndPoint);
                // start the server with a listen backlog of 100 connections
                listenSocket.Listen(m_maxConnectNum);
                // post accepts on the listening socket
                StartAccept(null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            foreach (AsyncUserToken token in m_clients)
            {
                try
                {
                    token.Socket.Shutdown(SocketShutdown.Both);
                }
                catch (Exception) { }
            }
            try
            {
                listenSocket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception) { }

            listenSocket.Close();
            int c_count = m_clients.Count;
            lock (m_clients) { m_clients.Clear(); }

            if (ClientNumberChange != null)
                ClientNumberChange(-c_count, null);
        }
        public void CloseClient(AsyncUserToken token)
        {
            try
            {
                token.Socket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception) { }
        }
        // Begins an operation to accept a connection request from the client 
        //
        // <param name="acceptEventArg">The context object to use when issuing 
        // the accept operation on the server's listening socket</param>
        public void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptEventArg_Completed);
            }
            else
            {
                // socket must be cleared since the context object is being reused
                acceptEventArg.AcceptSocket = null;
            }
            m_maxNumberAcceptedClients.WaitOne();
            if (!listenSocket.AcceptAsync(acceptEventArg))
            {
                ProcessAccept(acceptEventArg);
            }
        }

        // This method is the callback method associated with Socket.AcceptAsync 
        // operations and is invoked when an accept operation is complete
        //
        void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            try
            {
                Interlocked.Increment(ref m_clientCount);
                // Get the socket for the accepted client connection and put it into the 
                //ReadEventArg object user token
                SocketAsyncEventArgs readEventArgs = m_pool.Pop();
                AsyncUserToken userToken = (AsyncUserToken)readEventArgs.UserToken;
                userToken.Socket = e.AcceptSocket;
                userToken.ConnectTime = DateTime.Now;
                userToken.Remote = e.AcceptSocket.RemoteEndPoint;
                userToken.IPAddress = ((IPEndPoint)(e.AcceptSocket.RemoteEndPoint)).Address;

                lock (m_clients) { m_clients.Add(userToken); }

                if (ClientNumberChange != null)
                    ClientNumberChange(1, userToken);
                if (!e.AcceptSocket.ReceiveAsync(readEventArgs))
                {
                    ProcessReceive(readEventArgs);
                }
            }
            catch (Exception me)
            {
                logger.Error(me.Message + "\r\n" + me.StackTrace);
            }
            // Accept the next connection request
            if (e.SocketError == SocketError.OperationAborted) return;
            StartAccept(e);
        }
        void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            // determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }
        // This method is invoked when an asynchronous receive operation completes. 
        // If the remote host closed the connection, then the socket is closed.  
        // If data was received then the data is echoed back to the client.
        //
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            try
            {
                // check if the remote host closed the connection
                AsyncUserToken token = (AsyncUserToken)e.UserToken;
                if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
                {
                    //读取数据
                    byte[] data = new byte[e.BytesTransferred];
                    Array.Copy(e.Buffer, e.Offset, data, 0, e.BytesTransferred);

                    //将数据包交给后台处理,这里你也可以新开个线程来处理.加快速度.                    
                    if (ReceiveClientData != null)
                        ReceiveClientData(token, data);
                    //继续接收
                    if (!token.Socket.ReceiveAsync(e))
                        this.ProcessReceive(e);
                }
                else
                {
                    CloseClientSocket(e);
                }
            }
            catch (Exception xe)
            {
                logger.Error(xe.Message + "\r\n" + xe.StackTrace);                
            }
        }

        // This method is invoked when an asynchronous send operation completes.  
        // The method issues another receive on the socket to read any additional 
        // data sent from the client
        //
        // <param name="e"></param>
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // done echoing data back to the client
                AsyncUserToken token = (AsyncUserToken)e.UserToken;
                // read the next block of data send from the client
                bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                if (!willRaiseEvent)
                {
                    ProcessReceive(e);
                }
            }
            else
            {
                CloseClientSocket(e);
            }
        }
        //关闭客户端
        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = e.UserToken as AsyncUserToken;

            lock (m_clients) { m_clients.Remove(token); }
            //如果有事件,则调用事件,发送客户端数量变化通知
            if (ClientNumberChange != null)
                ClientNumberChange(-1, token);
            // close the socket associated with the client
            try
            {
                token.Socket.Shutdown(SocketShutdown.Send);
            }
            catch (Exception) { }
            token.Socket.Close();
            // decrement the counter keeping track of the total number of clients connected to the server
            Interlocked.Decrement(ref m_clientCount);
            m_maxNumberAcceptedClients.Release();
            // Free the SocketAsyncEventArg so they can be reused by another client
            e.UserToken = new AsyncUserToken();
            m_pool.Push(e);
        }
        /// <summary>
        /// 对数据进行打包,然后再发送
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public void SendMessage(AsyncUserToken token, byte[] message)
        {
            if (token == null || token.Socket == null || !token.Socket.Connected)
                return;
            try
            {
                //新建异步发送对象, 发送消息
                SocketAsyncEventArgs sendArg = new SocketAsyncEventArgs();
                sendArg.UserToken = token;
                sendArg.SetBuffer(message, 0, message.Length);  //将数据放置进去.
                token.Socket.SendAsync(sendArg);
            }
            catch (Exception e)
            {
                logger.Error("SendMessage - Error:" + e.Message);
            }
        }
    }
}
