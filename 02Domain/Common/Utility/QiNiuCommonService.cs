//using System;
//using System.Collections.Generic;
//using JSHC.IFramework.Base;
//using JSHC.IFramework.Utility.Helper;
//using JSHC.OrderingSystem.Common.Model;
//using Microsoft.Extensions.Options;

//namespace JSHC.OrderingSystem.Common.Utility
//{
//    public interface IQiNiuCommonService : IDependency
//    {
//        Tuple<bool, string> GetQiNiuUpToken ();

//        Tuple<bool, string> GetQiNiuHost ();
//    }
//    public class QiNiuCommonService : IQiNiuCommonService
//    {
//        private readonly IOptionsMonitor<AppSettings> _configuration;
//        private readonly IRedisCommonService _redisCommonService;

//        public QiNiuCommonService (IOptionsMonitor<AppSettings> configuration, IRedisCommonService redisCommonService)
//        {
//            _configuration = configuration;
//            _redisCommonService = redisCommonService;
//        }
//        public Tuple<bool, string> GetQiNiuUpToken ()
//        {
//            var qiNiuAccessKey = _configuration.CurrentValue.QiNiuAccessKey;

//            if (string.IsNullOrEmpty (qiNiuAccessKey))
//            {
//                return new Tuple<bool, string> (false, "QiNiuAccessKey不能为NULL，请检查appsettings.json->QiNiuAccessKey");
//            }

//            try
//            {
//                var dictionary = new Dictionary<string, dynamic>
//                    {
//                        {
//                        "QiNiuAccessKey",
//                        qiNiuAccessKey
//                        }
//                    };
//                var result = Helper.HttpRequestHelper.GateWaySync<Result<String>> ("ps.qiniu.gettoken", dictionary);
//                if (result.IsSuccess)
//                    return new Tuple<bool, string> (true, result.Data.Trim ('"'));

//                return new Tuple<bool, string> (false, result.ErrorMessage);
//            }
//            catch (Exception e)
//            {
//                return new Tuple<bool, string> (false, e.Message);
//            }
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public Tuple<bool, string> GetQiNiuHost ()
//        {
//            try
//            {
//                var cacheToken = CacheHelper.Get<string> ("qiniuhostkey");
//                var host = cacheToken.IsNullOrEmpty () ? _redisCommonService.Get<string> ("qiniuhostkey") : cacheToken;
//                if (!host.IsNullOrEmpty ())
//                    return new Tuple<bool, string> (true, host);
//                var result = Helper.HttpRequestHelper.GateWaySync<Result<String>> ("ps.qiniu.host.get", new Dictionary<string, dynamic> ());
//                if (result.IsSuccess)
//                {
//                    CacheHelper.Add ("qiniuhostkey", result.Data.Trim ('"'), 1440);
//                    _redisCommonService.Set ("qiniuhostkey", result.Data.Trim ('"'), TimeSpan.FromDays (1));
//                    return new Tuple<bool, string> (true, result.Data.Trim ('"'));
//                }

//                return new Tuple<bool, string> (false, "");
//            }
//            catch (Exception e)
//            {
//                return new Tuple<bool, string> (false, e.Message);
//            }
//        }
//    }
//}