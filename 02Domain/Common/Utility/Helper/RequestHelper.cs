//using Microsoft.AspNetCore.Http;

//namespace JSHC.OrderingSystem.Common.Utility.Helper
//{
//    public class RequestHelper
//    {
//        private static IHttpContextAccessor Accessor => new HttpContextAccessor();

//        #region 获取GET方式传入的值

//        /// <summary>
//        /// 获得指定Url参数的值
//        /// </summary>
//        /// <param name="queryItem">页面请求的参数名称</param>
//        /// <returns></returns>
//        public static string GetQueryString(string queryItem)
//        {
//            return GetQueryString(queryItem, "");
//        }

//        /// <summary>
//        /// 获取WEB页面传入的值(返回字符串类型)
//        /// </summary>
//        /// <param name="queryItem">页面请求的参数名称</param>
//        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
//        /// <returns></returns>
//        public static string GetQueryString(string queryItem, string defaultValue)
//        {


//            string _str = Accessor.HttpContext.Request.Query[queryItem];
//            if (string.IsNullOrEmpty(_str))
//            {
//                return defaultValue;
//            }

//            return _str;
//        }

//        /// <summary>
//        /// 获取WEB页面传入的值(返回整型)
//        /// </summary>
//        /// <param name="queryItem">页面请求的参数名称</param>
//        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
//        /// <returns>返回整型</returns>
//        public static int GetQueryInt32(string queryItem, int defaultValue)
//        {
//            queryItem = Accessor.HttpContext.Request.Query[queryItem];
//            int num;
//            if (!int.TryParse(queryItem, out num))
//            {
//                num = defaultValue;
//            }
//            return num;
//        }

//        /// <summary>
//        /// 获取WEB页面传入的值(返回双浮点型)
//        /// </summary>
//        /// <param name="queryItem"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        public static double GetQuerySingle(string queryItem, double defaultValue)
//        {
//            queryItem = Accessor.HttpContext.Request.Query[queryItem];
//            double num;
//            if (!double.TryParse(queryItem, out num))
//            {
//                num = defaultValue;
//            }
//            return num;
//        }

//        /// <summary>
//        /// 获取WEB页面传入的值(返回双浮点型)
//        /// </summary>
//        /// <param name="queryItem"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        public static decimal GetQueryDecimal(string queryItem, decimal defaultValue)
//        {
//            queryItem = Accessor.HttpContext.Request.Query[queryItem];
//            decimal num;
//            if (!decimal.TryParse(queryItem, out num))
//            {
//                num = defaultValue;
//            }
//            return num;
//        }


//        /// <summary>
//        /// 获取WEB页面传入的值
//        /// </summary>
//        /// <param name="queryItem"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        public static long GetQueryLong(string queryItem, long defaultValue)
//        {
//            queryItem = Accessor.HttpContext.Request.Query[queryItem];
//            long num;
//            if (!long.TryParse(queryItem, out num))
//            {
//                num = defaultValue;
//            }
//            return num;
//        }

//        #endregion

//        #region 获取POST方式传入的值

//        /// <summary>
//        /// 获得指定Url参数的值
//        /// </summary>
//        /// <param name="queryItem">页面请求的参数名称</param>
//        /// <returns></returns>
//        public static string GetFormString(string queryItem)
//        {
//            return GetFormString(queryItem, "");
//        }

//        /// <summary>
//        /// 获取WEB页面传入的值(返回字符串类型)
//        /// </summary>
//        /// <param name="queryItem">页面请求的参数名称</param>
//        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
//        /// <returns></returns>
//        public static string GetFormString(string queryItem, string defaultValue)
//        {
//            string _str = Accessor.HttpContext.Request.Form[queryItem];
//            if (string.IsNullOrEmpty(_str))
//            {
//                return defaultValue;
//            }

//            return _str;
//        }

//        /// <summary>
//        /// 获取WEB页面传入的值(返回整型)
//        /// </summary>
//        /// <param name="queryItem">页面请求的参数名称</param>
//        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
//        /// <returns>返回整型</returns>
//        public static int GetFormInt32(string queryItem, int defaultValue)
//        {
//            queryItem = Accessor.HttpContext.Request.Form[queryItem];
//            int num;
//            if (!int.TryParse(queryItem, out num))
//            {
//                num = defaultValue;
//            }
//            return num;
//        }

//        /// <summary>
//        /// 获取WEB页面传入的值(返回双浮点型)
//        /// </summary>
//        /// <param name="queryItem"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        public static double GetFormSingle(string queryItem, double defaultValue)
//        {
//            queryItem = Accessor.HttpContext.Request.Form[queryItem];
//            double num;
//            if (!double.TryParse(queryItem, out num))
//            {
//                num = defaultValue;
//            }
//            return num;
//        }

//        /// <summary>
//        /// 获取WEB页面传入的值(返回双浮点型)
//        /// </summary>
//        /// <param name="queryItem"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        public static decimal GetFormDecimal(string queryItem, decimal defaultValue)
//        {
//            queryItem = Accessor.HttpContext.Request.Form[queryItem];
//            decimal num;
//            if (!decimal.TryParse(queryItem, out num))
//            {
//                num = defaultValue;
//            }
//            return num;
//        }


//        /// <summary>
//        /// 获取WEB页面传入的值
//        /// </summary>
//        /// <param name="queryItem"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        public static long GetFormLong(string queryItem, long defaultValue)
//        {
//            queryItem = Accessor.HttpContext.Request.Form[queryItem];
//            long num;
//            if (!long.TryParse(queryItem, out num))
//            {
//                num = defaultValue;
//            }
//            return num;
//        }

//        #endregion
//    }
//}
