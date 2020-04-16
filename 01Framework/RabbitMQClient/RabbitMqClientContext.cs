using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQClient.Config;
using RabbitMQ.Client;

namespace RabbitMQClient
{
    /// <summary>
    /// RabbitMqClient数据上下文
    /// </summary>
    public class RabbitMqClientContext
    {
        /// <summary>
        /// 用于发送消息的Connection。
        /// </summary>
        public IConnection SendConnection { get; internal set; }

        /// <summary>
        /// 用于发送消息的Channel。
        /// </summary>
        public IModel SendChannel { get; internal set; }

        /// <summary>
        /// 用户侦听的Connection。
        /// </summary>
        public IConnection ListenConnection { get; internal set; }

        /// <summary>
        /// 用户侦听的Channel。
        /// </summary>
        public IModel ListenChannel { get; internal set; }

        /// <summary>
        /// 默认侦听的队列名称。
        /// </summary>
        public string ListenQueueName { get; internal set; }

        /// <summary>
        /// 实例编号。
        /// </summary>
        public string InstanceCode { get; set; }

    }


    public class RabbitMqClientFactory
    {

        /// <summary>
        /// 创建一个单例的RabbitMqClient实例。
        /// </summary>
        /// <returns>IRabbitMqClient</returns>
        public static IRabbitMqClient CreateRabbitMqClientInstance()
        {
            var rabbitMqClientContext = new RabbitMqClientContext
            {
                InstanceCode = Guid.NewGuid().ToString(),
                ListenQueueName = RabbitMqConfigFactory.CreateRabbitMqConfigInstance().MqListenQueueName
            };

            RabbitMqClient.Instance = new RabbitMqClient
            {
                Context = rabbitMqClientContext
            };

            return RabbitMqClient.Instance;
        }
        /// <summary>
        /// 创建一个IConnection。
        /// </summary>
        /// <returns></returns>
        internal static IConnection CreateConnection()
        {
            var mqConfigDom = RabbitMqConfigFactory.CreateRabbitMqConfigInstance(); //获取MQ的配置

            const ushort heartbeat = 60;
            var factory = new ConnectionFactory()
            {
                HostName = mqConfigDom.MqHost,
                Port = mqConfigDom.MqPort,
                UserName = mqConfigDom.MqUserName,
                Password = mqConfigDom.MqPassword,
                VirtualHost = mqConfigDom.MqVirtualHost,
                RequestedHeartbeat = heartbeat, //心跳超时时间
                AutomaticRecoveryEnabled = true //自动重连
            };

            return factory.CreateConnection(); //创建连接对象
        }

        /// <summary>
        /// 创建一个IModel。
        /// </summary>
        /// <param name="connection">IConnection.</param>
        /// <returns></returns>
        internal static IModel CreateModel(IConnection connection)
        {
            return connection.CreateModel();//创建通道
        }
    }
}
