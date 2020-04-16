using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQClient.Model;

namespace RabbitMQClient
{
    public interface IRabbitMqClient : IDisposable
    {
        /// <summary>
        /// RabbitMqClient数据上下文
        /// </summary>
        RabbitMqClientContext Context { get; set; }

        /// <summary>
        /// 消息被本地激活事件。通过绑定该事件来获取消息队列推送过来的消息。只能绑定一个事件处理程序。
        /// </summary>
        event Action<EventMessageResult> ActionEventMessage;


        /// <summary>
        /// 触发一个事件，向队列推送一个事件消息。
        /// </summary>
        /// <param name="messageBody">消息内容</param>
        /// <param name="exChange">Exchange名称</param>
        /// <param name="routingKey">路由关键字</param>
        void TriggerEventMessage(string messageBody, string exChange, string routingKey);

        /// <summary>
        /// 开始消息队列的默认监听。
        /// </summary>
        /// <param name="exChange">Exchange名称</param>
        /// <param name="routingKey">路由关键字</param>
        /// <param name="isDelay"></param>
        void ListenInit(string exChange, string routingKey, bool isDelay = false);
        
        /// <summary>
        /// 开始消息队列的默认监听。。
        /// </summary>
        /// <param name="queueName">消息队列名称</param>
        /// <param name="isDelay">消费失败是否重新加入队列</param>
        void ListenInit(string queueName, bool isDelay = false);

        /// <summary>
        /// 创建普通队列
        /// </summary>
        /// <param name="queueName">消息队列名称</param>
        void CreateRabbitQueue(string queueName);

        /// <summary>
        /// 创建延迟队列
        /// </summary>
        /// <param name="delayRabbitQueue"></param>
        void CreateDelayRabbitQueue(DelayRabbitQueue delayRabbitQueue);
    }
}
