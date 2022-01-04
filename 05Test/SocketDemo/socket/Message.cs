using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medicom.PASSPA2CollectService
{
    /// <summary>
    /// 消息实体类
    /// </summary>
    public class Message
    {
        public Message()
        {
            Content = new List<string>();
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MessageType { get; set; }
        /// <summary>
        /// 消息唯一ID
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// 消息段的集合
        /// </summary>
        public List<string> Content { get; set; }
    }
}
