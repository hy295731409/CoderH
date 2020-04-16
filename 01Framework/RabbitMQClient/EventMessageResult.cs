using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    /// <summary>
    /// 事件消息返回类型
    /// </summary>
    public class EventMessageResult
    {
        /// <summary>
        /// 完整消息内容
        /// </summary>
        public string MessageBody { get; set; }

        /// <summary>
        /// 原始消息的bytes。
        /// </summary>
        public byte[] MessageBytes { get; set; }

        /// <summary>
        /// 消息处理是否成功
        /// </summary>
        public bool IsOperationOk { get; set; }
    }
}
