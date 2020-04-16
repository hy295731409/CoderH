using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JSHC.IFramework.AutoMapper;
using JSHC.IFramework.Base;
using JSHC.IFramework.Encryption;
using JSHC.IFramework.Utility.Extension;

namespace JSHC.IFramework.Utility.Helper
{
    public class HttpRequestHelper
    {
        //网关默认地址
        private static readonly string GateWayHost = "http://openapi.tuluo56.com/gateway/";

        /// <summary>
        ///  当前登录用户票据
        /// </summary>
        public static readonly string BasicAuthTicketKey = "_BasicAuthTicketKey";

        static HttpRequestHelper()
        {
            var gateWayUrl = ConfigurationManager.AppSettings["GateWayUrl"];
            if (!gateWayUrl.IsNullOrEmpty())
                GateWayHost = gateWayUrl.TrimEnd('/') + "/";
        }

        /// <summary>
        /// POST请求数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postDatas"></param>
        /// <param name="encoding"></param>
        /// <param name="headerDatas"></param>
        /// <returns></returns>
        public static Result<string> SendPostRequest(string url, Dictionary<string, string> postDatas,
            Encoding encoding, Dictionary<string, string> headerDatas = null)
        {
            WebClient client = new WebClient();
            try
            {
                if (encoding == null)
                    encoding = Encoding.UTF8;
                string postString = GetParams(postDatas);
                byte[] postData = encoding.GetBytes(postString);
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                client.Headers.Add("ContentLength", postData.Length.ToString());
                if (headerDatas != null)
                {
                    foreach (var headerKey in headerDatas.Keys)
                    {
                        client.Headers.Add(headerKey, headerDatas[headerKey]);
                    }
                }
                var nvc = new NameValueCollection();
                foreach (var key in postDatas.Keys)
                {
                    nvc.Add(key, postDatas[key]);
                }
                byte[] responseData = client.UploadValues(url, "POST", nvc);
                return Result<string>.Success(encoding.GetString(responseData));
            }
            catch (Exception eeException)
            {
                return Result<string>.InnerError(eeException.Message);
            }
            finally
            {
                client.Dispose();
            }
        }

        public static Result<string> SendPostRequest(string url, Dictionary<string, string> postDatas,
            Dictionary<string, string> headerDatas = null)
        {
            return SendPostRequest(url, postDatas, Encoding.UTF8, headerDatas);
        }

        public static Result<string> SendPostRequest(string url, string postString, Encoding encoding,
            Dictionary<string, string> headerDatas = null)
        {
            WebClient client = new WebClient();
            try
            {
                if (encoding == null)
                    encoding = Encoding.UTF8;
                byte[] postData = encoding.GetBytes(postString);
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                client.Headers.Add("ContentLength", postData.Length.ToString());
                if (headerDatas != null)
                {
                    foreach (string headerKey in headerDatas.Keys)
                    {
                        client.Headers.Add(headerKey, headerDatas[headerKey]);
                    }
                }
                byte[] responseData = client.UploadData(url, "POST", postData);
                return Result<string>.Success(encoding.GetString(responseData));
            }
            catch (Exception eeException)
            {
                return Result<string>.InnerError(eeException.Message);
            }
            finally
            {
                client.Dispose();
            }
        }

        public static Result<string> SendPostRequest(string url, string postString,
            Dictionary<string, string> headerDatas = null)
        {
            return SendPostRequest(url, postString, Encoding.UTF8, headerDatas);
        }

        public static Result<string> SendRequest(string url, Encoding encoding,
            Dictionary<string, string> headerDatas = null)
        {
            WebClient client = new WebClient();
            try
            {
                if (encoding == null)
                    encoding = Encoding.UTF8;
                if (headerDatas != null)
                {
                    foreach (string headerKey in headerDatas.Keys)
                    {
                        client.Headers.Add(headerKey, headerDatas[headerKey]);
                    }
                }

                Byte[] pageData = client.DownloadData(url);

                string resultstring = encoding.GetString(pageData);

                return Result<string>.Success(resultstring);
            }
            catch (Exception eeException)
            {
                return Result<string>.InnerError(eeException.Message);
            }
            finally
            {
                client.Dispose();
            }
        }

        public static Result<string> SendRequest(string url)
        {
            return SendRequest(url, Encoding.UTF8);
        }

        private static string GetParams(Dictionary<string, string> dic)
        {
            List<string> list = new List<string>();
            foreach (string key in dic.Keys)
            {
                if (dic[key] != null)
                    list.Add(key + "=" + dic[key]);
            }
            return string.Join("&", list.ToArray());
        }


        #region GateWay

        #region private


        public static async Task<Dictionary<string, dynamic>> GetHeadersParams(string serviceName,
            Dictionary<string, dynamic> postData)
        {
            var jshcAppId = ConfigurationManager.AppSettings["JSHC_AppId"];
            var jshcAppSecret = ConfigurationManager.AppSettings["JSHC_AppSecret"];
            var jshcTimestamp = await GetTimeStamp();
            var jshcEchostr = Guid.NewGuid().ToString("N");

            var signatureList = new List<string>
            {
                "ServiceName=" + serviceName,
                "JSHC_AppId=" + jshcAppId,
                "JSHC_Timestamp=" + jshcTimestamp,
                "JSHC_Echostr=" + jshcEchostr
            };

            //body参数处理
            foreach (var keyValuePair in postData)
            {
                signatureList.Add(keyValuePair.Key + "=" + keyValuePair.Value);
            }

            signatureList.Sort();
            var signatureArray = signatureList.ToArray();
            Array.Sort(signatureArray, (a, b) => string.Compare(a, b, StringComparison.Ordinal));

            var str = string.Join("_", signatureArray);

            var signature = await SecurityHelper.Hmacsha256Encrypt(str, jshcAppSecret, Encoding.UTF8);

            return new Dictionary<string, dynamic>
            {
                {"JSHC_AppId", jshcAppId},
                {"JSHC_Signature", signature},
                {"JSHC_Timestamp", jshcTimestamp},
                {"JSHC_Echostr", jshcEchostr}
            };
        }

        /// <summary> 
        /// 获取时间戳 
        /// </summary> 
        /// <returns></returns> 
        private static async Task<string> GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return await Task.FromResult(Convert.ToInt64(ts.TotalSeconds).ToString());
        }

        #endregion

        /// <summary>
        /// 统一请求GateWay
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="paras">参数</param>
        /// <param name="headers">header参数</param>
        /// <returns>返回字符串</returns>
        public static string GateWaySync(string serviceName, Dictionary<string, dynamic> paras, Dictionary<string, dynamic> headers = null)
        {
            return GateWayAsync(serviceName, paras, headers).Result;
        }

        /// <summary>
        /// 统一请求GateWay
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="paras">参数</param>
        /// <param name="headers">header参数</param>
        /// <returns></returns>
        public static async Task<string> GateWayAsync(string serviceName, Dictionary<string, dynamic> paras, Dictionary<string, dynamic> headers = null)
        {
            var apiClient = new HttpClient();
            try
            {
                var gateWayUrl = GateWayHost + serviceName;
                var jshcAppId = ConfigurationManager.AppSettings["JSHC_AppId"];
                var jshcAppSecret = ConfigurationManager.AppSettings["JSHC_AppSecret"];
                if (jshcAppId.IsNullOrEmpty() || jshcAppSecret.IsNullOrEmpty())
                    //报错返回结构一致
                    return await Task.FromResult(Result.InnerError("请在config中设置JSHC_AppId或者JSHC_AppSecret").ToJson()).ConfigureAwait(false);

                var headerDatas = await GetHeadersParams(serviceName, paras);
                var dic = paras.MapTo<Dictionary<string, string>>();
                HttpContent httpcontent = new FormUrlEncodedContent(dic);
                foreach (var headerKey in headerDatas.Keys)
                {
                    httpcontent.Headers.Add(headerKey, headerDatas[headerKey]);
                }
                if (headers != null && headers.Any())
                {
                    foreach (var header in headers)
                    {
                        httpcontent.Headers.Add(header.Key, header.Value);
                    }
                }

                var response = await apiClient.PostAsync(gateWayUrl, httpcontent).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    return await Task.FromResult(Result.InnerError(response.ReasonPhrase).ToJson()).ConfigureAwait(false);

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return await Task.FromResult(content).ConfigureAwait(false);

            }
            catch (Exception e)
            {
                return await Task.FromResult(Result.InnerError(e.Message).ToJson()).ConfigureAwait(false);
            }
            finally
            {
                apiClient.Dispose();
            }
        }


        /// <summary>
        /// 统一请求GateWay
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="paras"></param>
        /// <param name="headers">header参数</param>
        /// <returns>返回JSON对象</returns>
        public static T GateWaySync<T>(string serviceName, Dictionary<string, dynamic> paras, Dictionary<string, dynamic> headers = null) where T : new()
        {
            var result = GateWaySync(serviceName, paras, headers);
            return result.JsonToObj<T>();
        }

        #endregion
    }
}