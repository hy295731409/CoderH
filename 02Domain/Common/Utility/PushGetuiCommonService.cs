using System;
using System.Collections.Generic;
using JSHC.IFramework.Base;
using JSHC.IFramework.Utility.Helper;

namespace JSHC.OrderingSystem.Common.Utility
{
    /// <summary>
    /// 个推消息推送相关处理
    /// </summary>
    public class PushGetuiCommonService
    {
        /// <summary>
        /// 单个ClientId绑定别名
        /// </summary>
        /// <param name="accountId">用户主键</param>
        /// <param name="clientId">客户端ID</param>
        /// <returns></returns>
        public static Result<string> BindAlias(long accountId, string clientId)
        {
            var dictionary = new Dictionary<string, dynamic> {
                {"accountId", accountId},
                {"clientId", clientId}
            };
            var result = HttpRequestHelper.GateWaySync<Result<string>>("ps.push.alias.bind", dictionary);
            return result;
        }
        /// <summary>
        /// 根据别名获取clientId信息
        /// </summary>
        /// <param name="accountId">用户主键</param>
        /// <returns></returns>
        public static Result<string> QueryClientId(long accountId)
        {
            var dictionary = new Dictionary<string, dynamic> {
                {"accountId", accountId}
            };
            var result = HttpRequestHelper.GateWaySync<Result<string>>("ps.push.alias.get", dictionary);
            return result;
        }
        /// <summary>
        /// 绑定别名的所有ClientId解绑
        /// </summary>
        /// <param name="accountId">用户主键</param>
        /// <returns></returns>
        public static Result<string> AliasUnBindAll(long accountId)
        {
            var dictionary = new Dictionary<string, dynamic> {
                {"accountId", accountId}
            };
            var result = HttpRequestHelper.GateWaySync<Result<string>>("ps.push.alias.unbind", dictionary);
            return result;
        }
        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="accountId">用户主键</param>
        /// <param name="transmissionContent">透传内容</param>
        /// <param name="content">通知栏内容</param>
        /// <returns></returns>
        public static Result<string> PushMessageToApp(long accountId, string transmissionContent, string content)
        {
            var dictionary = new Dictionary<string, dynamic> {
                {"AccountId", accountId},
                {"TransmissionContent", transmissionContent},
                {"Content", content}
            };
            var result = HttpRequestHelper.GateWaySync<Result<string>>("ps.push.send", dictionary);
            return result;
        }
    }
}
