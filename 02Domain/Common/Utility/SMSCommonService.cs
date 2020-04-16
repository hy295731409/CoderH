using System;
using System.Collections.Generic;
using JSHC.IFramework.Base;
using JSHC.OrderingSystem.Common.Model;
using JSHC.OrderingSystem.Common.Utility.Helper;
using Microsoft.Extensions.Options;

namespace JSHC.OrderingSystem.Common.Utility
{
    public interface ISmsCommonService: IDependency
    {

        #region 发送验证码

        /// <summary>
        /// 发送注册短信验证码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Tuple<bool, string> SendRegisteSCode(string phoneNumber);

        /// <summary>
        /// 发送重置支付密码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Tuple<bool, string> SendSetPayPasswordCode(string phoneNumber);

        /// <summary>
        /// 发送重置密码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Tuple<bool, string> SendSetPasswordCode(string phoneNumber);

        /// <summary>
        /// 发送实名认证验证码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Tuple<bool, string> SendAccountAuthenCode(string phoneNumber);

        /// <summary>
        /// 发送订货系统供货端管理员绑定手机号验证码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Tuple<bool, string> SendAdminBindTelCode(string phoneNumber);
        #endregion

        #region 验证验证码

        /// <summary>
        /// 验证短信
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="code"></param>
        /// <param name="type">Constant.SmsCodeType</param>
        /// <returns></returns>
        bool ValidSmsCode(string phoneNumber, string code, string type);

        #endregion

        #region 发送消息

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="temKey">参考本类静态变量</param>
        /// <param name="param">例如：{Starting:'四川成都',Destination:'重庆',CargoType:'粮油',u:'10',t:'300',m:'100',price:'10000'}</param>
        /// <returns></returns>
        Tuple<bool, string> SendSmsMessage(string phoneNumber, string temKey, string param);

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="temKey"></param>
        /// <param name="codeType">Constant.SmsCodeType</param>
        /// <param name="param"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Tuple<bool, string> SendSmsCode(string phoneNumber, string temKey, string codeType, string param, string code);

        #endregion
    }

    public  class SmsCommonService:ISmsCommonService
    {
        private readonly IOptionsMonitor<AppSettings> _configuration;
        private readonly IRedisCommonService _redisCommonService;

        public SmsCommonService(IOptionsMonitor<AppSettings> configuration, IRedisCommonService redisCommonService)
        {
            _configuration = configuration;
            _redisCommonService = redisCommonService;
        }

        private static string Plat_Register = "SMS_62750318";
        private static string Plat_SetPayPwd = "SMS_62595204";
        private static string Plat_SetPwd = "SMS_62735500";

        private static string SmsCode_Store_PreKey = "sms_";

        /// <summary>
        /// 店铺新订单
        /// </summary>
        public static string Shop_New_Order = "SMS_12465099";

        /// <summary>
        /// 店铺审核失败
        /// </summary>
        public static string Shop_ValidFalse = "SMS_12465098";

        /// <summary>
        /// 店铺审核通过
        /// </summary>
        public static string Shop_ValidTrue = "SMS_12460155";

        /// <summary>
        /// OTO推送货源模板
        /// </summary>
        public static string OTO_Push_Goods = "SMS_14210352";

        /// <summary>
        /// 实名认证验证码
        /// </summary>
        public static string Plat_AccountAuthen = "SMS_62755320";

        /// <summary>
        /// tms实名认证验证码
        /// </summary>
        public static string Tms_AccountAuthen = "SMS_32005001";

        /// <summary>
        /// 通知用户线下支付银行卡号
        /// </summary>
        public static string Pay_Notify_Bank = "SMS_41960095";

        /// <summary>
        /// 物资采购通知商家发货
        /// </summary>
        public static string Eb_Notify_Vendor_Send = "SMS_42155043";

        /// <summary>
        /// OS供货端管理员绑定手机号
        /// </summary>
        public static string Os_Admin_BindTel = "SMS_143705623";

        #region 发送验证码

        /// <summary>
        /// 发送注册短信验证码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Tuple<bool, string> SendRegisteSCode(string phoneNumber)
        {
            return Send(phoneNumber, Plat_Register, SmsCodeType.Register, ESmsType.验证码, string.Empty);
        }

        /// <summary>
        /// 发送重置支付密码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Tuple<bool, string> SendSetPayPasswordCode(string phoneNumber)
        {
            return Send(phoneNumber, Plat_SetPayPwd, SmsCodeType.ResetPayPassword, ESmsType.验证码, string.Empty);
        }

        /// <summary>
        /// 发送重置密码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public  Tuple<bool, string> SendSetPasswordCode(string phoneNumber)
        {
            return Send(phoneNumber, Plat_SetPwd, SmsCodeType.ResetPassword, ESmsType.验证码, string.Empty);
        }

        /// <summary>
        /// 发送实名认证验证码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public  Tuple<bool, string> SendAccountAuthenCode(string phoneNumber)
        {
            return Send(phoneNumber, Plat_AccountAuthen, SmsCodeType.AccountAuthen, ESmsType.验证码, string.Empty);
        }

        /// <summary>
        /// 发送订货系统供货端管理员绑定手机号验证码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Tuple<bool, string> SendAdminBindTelCode(string phoneNumber)
        {
            return Send(phoneNumber, Os_Admin_BindTel, SmsCodeType.BindAdminTel, ESmsType.验证码, string.Empty);
        }
        #endregion

        #region 验证验证码

        /// <summary>
        /// 验证短信
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="code"></param>
        /// <param name="type">Constant.SmsCodeType</param>
        /// <returns></returns>
        public  bool ValidSmsCode(string phoneNumber, string code, string type)
        {
            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(code))
                return false;

            var storeKey = SmsCode_Store_PreKey + phoneNumber + "_" + type;
            var getCode = _redisCommonService.Get<string>(storeKey);

            if (!string.IsNullOrEmpty(getCode) && code.Equals(getCode))
            {
                //验证一次，删除该key
                _redisCommonService.Del(storeKey);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 发送消息

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="temKey">参考本类静态变量</param>
        /// <param name="param">例如：{Starting:'四川成都',Destination:'重庆',CargoType:'粮油',u:'10',t:'300',m:'100',price:'10000'}</param>
        /// <returns></returns>
        public  Tuple<bool, string> SendSmsMessage(string phoneNumber, string temKey, string param)
        {
            return Send(phoneNumber, temKey, string.Empty, ESmsType.通知, param);
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="temKey">阿里大于短信模板key</param>
        /// <param name="codeType">Constant.SmsCodeType 短信验证码类型</param>
        /// <param name="param"></param>
        /// <param name="code">需要发送的验证码</param>
        /// <returns></returns>
        public  Tuple<bool, string> SendSmsCode(string phoneNumber, string temKey, string codeType, string param, string code)
        {
            return Send(phoneNumber, temKey, codeType, ESmsType.验证码, param, code);
        }

        #endregion

        #region 统一发送公共方法

        private Tuple<bool, string> Send(string phoneNumber, string smsTempCode, string smsCodeType, ESmsType type, string param, string code = "")
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return new Tuple<bool, string>(false, "手机号码为空");

            if (type == ESmsType.验证码)
            {

                //手机号码加验证码类型
                var storeKey = SmsCode_Store_PreKey + phoneNumber + "_" + smsCodeType;//sms_13000000000_6

                //写入redis
                var result = _redisCommonService.Set<string>(storeKey, code, TimeSpan.FromMinutes(10));
                if (!result)
                {
                    //记录日志
                    return new Tuple<bool, string>(false, "写入缓存失败");
                }
            }

            try
            {
                var dictionary = new Dictionary<string, dynamic> {
                    {"phoneNumber", phoneNumber},
                    {"smsTempCode", smsTempCode},
                    {"code", code},
                    {"param", param}
                };
                var sendResult = Helper.HttpRequestHelper.GateWaySync<Result>("ps.alidayu.send", dictionary);

                if (sendResult.IsSuccess)
                {
                    return new Tuple<bool, string>(true, "");
                }

                return new Tuple<bool, string>(false, sendResult.ErrorMessage);
            }
            catch (Exception e)
            {
                return new Tuple<bool, string>(false, e.Message);
            }
        }

        #endregion
    }

    /// <summary>
    /// 短信类型
    /// </summary>
    public enum ESmsType
    {
        验证码 = 1,
        通知 = 2
    }
    /// <summary>
    /// 短信验证码类型
    /// </summary>
    public class SmsCodeType
    {
        /// <summary>
        /// 注册
        /// </summary>
        public const string Register = "1";

        /// <summary>
        /// 重置密码
        /// </summary>
        public const string ResetPassword = "2";

        /// <summary>
        /// 重置支付密码
        /// </summary>
        public const string ResetPayPassword = "3";

        /// <summary>
        /// 实名认证验证码
        /// </summary>
        public const string AccountAuthen = "4";
        
        /// <summary>
        /// 登录验证码
        /// </summary>
        public const string AccountLogin = "5";

        /// <summary>
        /// 绑定管理员手机号
        /// </summary>
        public const string BindAdminTel = "6";
    }
}
