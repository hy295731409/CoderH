using System;
using System.Configuration;

namespace RabbitMQClient.Config
{
    /// <summary>
    /// RabbitMQ消息队列相关配置
    /// </summary>
    public class RabbitMqConfig
    {
        /// <summary>
        ///  RabbitMQ消息队列的地址。
        /// </summary>
        public string MqHost { get; set; }

        /// <summary>
        ///  RabbitMQ消息队列的端口
        /// </summary>
        public int MqPort { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string MqUserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string MqPassword { get; set; }

        /// <summary>
        /// Virtual host
        /// </summary>
        public string MqVirtualHost { get; set; }

        /// <summary>
        /// 客户端默认监听的队列名称
        /// </summary>
        public string MqListenQueueName { get; set; }
    }


    internal class RabbitMqConfigFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static RabbitMqConfig CreateRabbitMqConfigInstance()
        {
            return GetConfigFormAppStting();
        }
        /// <summary>
        /// 读取config的配置项
        /// </summary>
        /// <returns></returns>
        private static RabbitMqConfig GetConfigFormAppStting()
        {
            var result = new RabbitMqConfig();

            var mqHost = ConfigurationManager.AppSettings["MqHost"];
            if (string.IsNullOrEmpty(mqHost))
                throw new Exception("RabbitMQ地址配置错误");
            result.MqHost = mqHost;
            var mqPort = 5672;
            if (!int.TryParse(ConfigurationManager.AppSettings["MqPort"],out mqPort))
                throw new Exception("RabbitMQ端口配置错误");
            result.MqPort = mqPort;

            var mqUserName = ConfigurationManager.AppSettings["MqUserName"];
            if (string.IsNullOrEmpty(mqUserName))
                throw new Exception("RabbitMQ用户名不能为NULL");

            result.MqUserName = mqUserName;

            var mqPassword = ConfigurationManager.AppSettings["MqPassword"];
            if (string.IsNullOrEmpty(mqPassword))
                throw new Exception("RabbitMQ密码不能为NULL");

            result.MqPassword = mqPassword;


            var mqVirtualHost = ConfigurationManager.AppSettings["MqVirtualHost"];
            if (string.IsNullOrEmpty(mqVirtualHost))
                throw new Exception("VirtualHost不能为NULL");

            result.MqVirtualHost = mqVirtualHost;


            var mqListenQueueName = ConfigurationManager.AppSettings["MqListenQueueName"];
            if (string.IsNullOrEmpty(mqListenQueueName))
                throw new Exception("MqListenQueueName不能为NULL");

            result.MqListenQueueName = mqListenQueueName;

            return result;
        }
    }

}
