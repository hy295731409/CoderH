using log4net;
using Medicom.Common.DatabaseDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medicom.PASSPA2CollectService
{
    public class AcceptMessage
    {
        public SocketManager socket;
        public static readonly ILog logger = LogManager.GetLogger(typeof(AcceptMessage));
        //存放消息的队列
        public Queue<string> TaskQueue;
        public AcceptMessage()
        {
            TaskQueue = new Queue<string>();
        }
        public void Run()
        {
            //需要监听的IP地址
            //string ip = Config.IP; 
            string ip = "172.18.5.122"; 
            //需要监听的I端口
            //int port = Config.PORT; 
            int port = 5556; 
            socket = new SocketManager(10, 1024 * 1024);
            socket.ReceiveClientData += ReceiveMessage;
            socket.Init();
            socket.Start(new IPEndPoint(IPAddress.Parse(ip), port));
            logger.InfoFormat("正在监听{0}:{1}......", ip, port);
            return;
        }
        public void ReceiveMessage(AsyncUserToken token, byte[] buff)
        {
            var message = Encoding.UTF8.GetString(buff, 0, buff.Length).Replace("\0", "").TrimEnd();

            if (!string.IsNullOrEmpty(message))
            {
                logger.DebugFormat("收到{0}消息：{1}", token.Remote, message);
                //这里需要给他们His反馈消息，如果他们收不到某个消息的反馈，会一直给咱们服务发送该条消息
                var task = ReturnEntity(message);
                logger.Info("发送到远程地址：" + token.Remote);
                //回发给his
                socket.SendMessage(token, GetAckMessage(task));
                //将收到的消息加入队列
                TaskQueue.Enqueue(message);
            }
        }
        /// <summary>
        /// 获取反馈的消息内容
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public byte[] GetAckMessage(Message entity)
        {
            try
            {
                var date = DateTime.Now.ToString("yyyyMMddHHmm");

                var builder = new StringBuilder();
                builder.Append(string.Format("MSH|^~\\&|EMR||{0}||{1}||{2}|{3}|P|2.4", "Medicom", date, entity.MessageType, Guid.NewGuid().ToString().Replace("-", "")));
                builder.Append('\x0d'.ToString());
                builder.Append(string.Format("MSA|AA|{0}|执行成功", entity.MessageId));
                var content = builder.ToString();
                char[] chars = new char[content.Length + 3];
                chars[0] = '\x0b';
                chars[chars.Length - 2] = '\x1c';
                chars[chars.Length - 1] = '\x0d';
                for (int i = 0; i < content.Length; i++)
                {
                    chars[i + 1] = content[i];
                }

                logger.InfoFormat("ACK消息：" + new string(chars, 0, chars.Length));

                return Encoding.UTF8.GetBytes(chars);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Message ReturnEntity(string message)
        {
            var entity = Helper.ReturnEntity(message);
            if (entity.MessageType.Contains("ZMR"))
                entity.MessageType = entity.MessageType.Replace("ZMR", "ACK");
            if (entity.MessageType.Contains("ADT"))
                entity.MessageType = entity.MessageType.Replace("ADT", "ACK");
            if (entity.MessageType.Contains("MFN"))
                entity.MessageType = entity.MessageType.Replace("MFN", "MFK");
            return entity;
        }
        /// <summary>
        /// 开启新的线程从队列里取数据并处理
        /// </summary>
        public void TaskBegin()
        {
            while (true)
            {
                if (TaskQueue.Count > 0)
                {
                    var message = TaskQueue.Dequeue();
                    Task.Factory.StartNew(() => new Analysis(message).Action());
                }
            }
        }
    }
}
