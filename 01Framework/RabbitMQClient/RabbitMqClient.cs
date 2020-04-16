using RabbitMQClient.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public class RabbitMqClient : IRabbitMqClient
    {


        #region Static fields

        /// <summary>
        /// 客户端实例私有字段。
        /// </summary>
        private static IRabbitMqClient _instanceClient;

        /// <summary>
        /// 返回全局唯一的RabbitMqClient实例
        /// </summary>
        public static IRabbitMqClient Instance
        {
            get
            {
                if (_instanceClient == null)
                    RabbitMqClientFactory.CreateRabbitMqClientInstance();

                return _instanceClient;
            }

            internal set => _instanceClient = value;
        }

        /// <summary>
        /// 当侦听的队列中有消息到达时触发的执行事件。
        /// </summary>
        public event Action<EventMessageResult> ActionEventMessage;

        #endregion

        /// <summary>
        /// RabbitMqClient数据上下文
        /// </summary>
        public RabbitMqClientContext Context { get; set; }

        #region 发送消息

        /// <summary>
        /// 触发一个事件，向队列推送一个事件消息。
        /// </summary>
        /// <param name="messageBody">消息内容</param>
        /// <param name="exChange">Exchange名称</param>
        /// <param name="routingKey">路由关键字</param>
        public void TriggerEventMessage(string messageBody, string exChange, string routingKey)
        {
            if (string.IsNullOrEmpty(messageBody))
                throw new Exception("消息内容不能为空");
            if (string.IsNullOrEmpty(routingKey))
                throw new Exception("路由关键字不能为空");

            //获取连接
            Context.SendConnection = RabbitMqClientFactory.CreateConnection();

            using (Context.SendConnection)
            {
                //获取通道
                Context.SendChannel = RabbitMqClientFactory.CreateModel(Context.SendConnection);
                if (!string.IsNullOrEmpty(exChange))
                {
                    Context.SendChannel.ExchangeDeclare(exChange, "direct", durable: true);
                }
                const byte deliveryMode = 2;
                using (Context.SendChannel)
                {

                    var body = Encoding.UTF8.GetBytes(messageBody);
                    var properties = Context.SendChannel.CreateBasicProperties();
                    properties.DeliveryMode = deliveryMode; //表示持久化消息
                    //推送消息
                    Context.SendChannel.BasicPublish(exChange, routingKey, properties, body);
                }
            }
        }

        /// <summary>
        /// 延迟消息
        /// </summary>
        /// <param name="messageBody">消息内容</param>
        /// <param name="queueName">队列名称</param>
        public void SendDelayMessage(string messageBody, string queueName)
        {
            //获取连接
            Context.SendConnection = RabbitMqClientFactory.CreateConnection();
            using (Context.SendConnection)
            {
                Context.SendChannel = RabbitMqClientFactory.CreateModel(Context.SendConnection);
                using (Context.SendChannel)
                {
                    CreateRabbitQueue(queueName);
                    var body = Encoding.UTF8.GetBytes(messageBody);

                    //推送消息
                    Context.SendChannel.BasicPublish("", queueName, null, body);
                }
            }
        }

        #endregion

        #region 接收消息

        /// <summary>
        /// 侦听初始化。
        /// </summary>
        /// <param name="exChange">Exchange名称</param>
        /// <param name="routingKey">路由关键字</param>
        /// <param name="isDelay">消费失败是否重新加入队列</param>
        public void ListenInit(string exChange, string routingKey, bool isDelay = false)
        {
            //获取连接
            Context.ListenConnection = RabbitMqClientFactory.CreateConnection();
            Context.ListenConnection.ConnectionShutdown += (o, e) => throw new Exception("connection shutdown:" + e.ReplyText);
            //获取通道
            Context.ListenChannel = RabbitMqClientFactory.CreateModel(Context.ListenConnection);

            CreateRabbitQueue(Context.ListenQueueName);
            Context.ListenChannel.QueueBind(Context.ListenQueueName, exChange, routingKey);

            var consumer = new EventingBasicConsumer(Context.ListenChannel); //创建事件驱动的消费者类型

            if (isDelay)
                consumer.Received += consumer_Received;
            else
                consumer.Received += consumer_Received_Delay;

            Context.ListenChannel.BasicQos(0, 1, false); //一次只获取一个消息进行消费
            Context.ListenChannel.BasicConsume(Context.ListenQueueName, false, consumer);
        }

        /// <summary>
        /// 侦听初始化。
        /// </summary>
        /// <param name="queueName">消息队列名称</param>
        /// <param name="isDelay">消费失败是否重新加入队列</param>
        public void ListenInit(string queueName, bool isDelay = false)
        {
            //获取连接
            Context.ListenConnection = RabbitMqClientFactory.CreateConnection();
            Context.ListenConnection.ConnectionShutdown += (o, e) => throw new Exception("connection shutdown:" + e.ReplyText);

            //获取通道
            Context.ListenChannel = RabbitMqClientFactory.CreateModel(Context.ListenConnection);

            var consumer = new EventingBasicConsumer(Context.ListenChannel); //创建事件驱动的消费者类型

            if (isDelay)
                consumer.Received += consumer_Received;
            else
                consumer.Received += consumer_Received_Delay;

            Context.ListenChannel.BasicConsume(queueName, false, consumer);
        }

        /// <summary>
        /// 正常接受消息，消费失败重新加入队列，供其他消费者消费
        /// </summary>
        private void consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            try
            {

                var result = new EventMessageResult
                {
                    MessageBytes = e.Body,
                    MessageBody = Encoding.UTF8.GetString(e.Body)
                };

                ActionEventMessage?.Invoke(result);

                if (!result.IsOperationOk)
                {
                    //未能消费此消息，重新放入队列头
                    Context.ListenChannel.BasicReject(e.DeliveryTag, true);
                }
                else if (!Context.ListenChannel.IsClosed)
                {
                    Context.ListenChannel.BasicAck(e.DeliveryTag, false);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// 消费失败不重新加入队列，其他消费者无法消费，只能进入死信路由，供延迟队列服务处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void consumer_Received_Delay(object sender, BasicDeliverEventArgs e)
        {
            try
            {

                var result = new EventMessageResult
                {
                    MessageBytes = e.Body,
                    MessageBody = Encoding.UTF8.GetString(e.Body)
                };

                ActionEventMessage?.Invoke(result);

                if (!result.IsOperationOk)
                {
                    //未能消费此消息,不重新放入队列头，直接进入死信路由
                    Context.ListenChannel.BasicReject(e.DeliveryTag, false);
                }
                else if (!Context.ListenChannel.IsClosed)
                {
                    Context.ListenChannel.BasicAck(e.DeliveryTag, false);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region 创建队列

        #region 普通队列
        /// <summary>
        /// 创建普通队列
        /// </summary>
        /// <param name="queueName">消息队列名称</param>
        public void CreateRabbitQueue(string queueName)
        {
            CreateRabbitQueue(queueName, null);
        }


        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="queueName">消息队列名称</param>
        /// <param name="arguments">消息队列的特性，如：过期时间等</param>
        private void CreateRabbitQueue(string queueName, Dictionary<string, object> arguments)
        {
            //获取连接
            Context.SendConnection = RabbitMqClientFactory.CreateConnection();

            using (Context.SendConnection)
            {
                //获取通道
                Context.SendChannel = RabbitMqClientFactory.CreateModel(Context.SendConnection);
                using (Context.SendChannel)
                {
                    Context.SendChannel.QueueDeclare(queueName, true, false, false, arguments);
                }
            }
        }

        #endregion

        #region 延迟队列
        /// <summary>
        /// 创建延迟队列
        /// </summary>
        /// <param name="delayRabbitQueue"></param>
        public void CreateDelayRabbitQueue(DelayRabbitQueue delayRabbitQueue)
        {
            if (delayRabbitQueue == null) throw new Exception("创建延迟队列失败");
            var dic = new Dictionary<string, object>
                {
                    {"x-expires", delayRabbitQueue.Expires},
                    {"x-message-ttl", delayRabbitQueue.MessageTtl}, //队列上消息过期时间，应小于队列过期时间  
                    {"x-dead-letter-exchange", delayRabbitQueue.Exchange},//过期消息转向路由  
                    {"x-dead-letter-routing-key", delayRabbitQueue.RoutingKey}//过期消息转向路由相匹配routingkey  
                };
            CreateRabbitQueue(delayRabbitQueue.QueueName, dic);
        }

        #endregion

        #endregion

        #region IDispose

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            if (Context.SendConnection == null) return;

            if (Context.SendConnection.IsOpen)
                Context.SendConnection.Close();

            Context.SendConnection.Dispose();
        }

        #endregion
    }
}
