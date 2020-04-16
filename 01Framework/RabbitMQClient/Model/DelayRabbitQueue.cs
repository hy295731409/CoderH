using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient.Model
{
    public class DelayRabbitQueue
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 队列过期时间(毫秒)
        /// </summary>
        public int Expires { get; set; }

        /// <summary>
        /// 队列上消息过期时间(毫秒)
        /// </summary>
        public int MessageTtl { get; set; }

        /// <summary>
        /// 过期消息转向路由  
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// 过期消息转向路由相匹配routingkey  
        /// </summary>
        public string RoutingKey { get; set; }
    }
}
