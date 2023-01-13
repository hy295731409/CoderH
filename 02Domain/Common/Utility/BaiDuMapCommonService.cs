//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using JSHC.IFramework.Base;
//using JSHC.IFramework.Utility.Extension;
//using JSHC.IFramework.Utility.Helper;
//using JSHC.OrderingSystem.Common.Model;
//using JSHC.OrderingSystem.Common.Utility.MapModel;
//using Microsoft.Extensions.Options;

//namespace JSHC.OrderingSystem.Common.Utility
//{
//    /// <summary>
//    /// 百度地图API服务
//    /// </summary>
//    public interface IBaiDuMapCommonService: IDependency
//    {
//        #region 驾车距离计算

//        /// <summary>
//        /// 根据坐标获取驾车距离
//        /// </summary>
//        /// <param name="originsLat">出发地纬度</param>
//        /// <param name="originsLng">出发地经度</param>
//        /// <param name="destinationsLat">目的地纬度</param>
//        /// <param name="destinationsLng">目的地经度</param>
//        /// <returns></returns>
//        Result<MapDistance> GetDrivingDistance(string originsLat, string originsLng,
//            string destinationsLat, string destinationsLng);

//        /// <summary>
//        /// 根据坐标获取驾车距离
//        /// </summary>
//        /// <param name="origins">出发地标准坐标</param>
//        /// <param name="destinations">目的地标准坐标</param>
//        /// <returns></returns>
//        Result<MapDistance> GetDrivingDistance(string origins, string destinations);

//        #endregion

//        #region 地理编码查询

//        /// <summary>
//        /// 地理编码查询
//        /// </summary>
//        /// <param name="address">检索地址</param>
//        /// <param name="city">城市</param>
//        /// <returns></returns>
//        Result<GeocoderResult> GetGeocoder(string address, string city = "北京市");

//        /// <summary>
//        /// 逆地理编码服务
//        /// </summary>
//        /// <param name="location">坐标地址</param>
//        /// <returns></returns>
//        Result<ReverseGeocodingReuslt> GetGeocoder(string location);

//        /// <summary>
//        /// 逆地理编码服务
//        /// </summary>
//        /// <param name="lat">纬度</param>
//        /// <param name="lng">经度</param>
//        /// <returns></returns>
//        Result<ReverseGeocodingReuslt> GetGeocoderForLatAndLng(string lat, string lng);

//        #endregion

//        #region 输入关键字辅助信息查询

//        /// <summary>
//        /// 输入关键字辅助信息查询
//        /// </summary>
//        /// <param name="query">检索关键字</param>
//        /// <param name="city">城市</param>
//        /// <returns></returns>
//        Result<List<SuggestionResult>> GetSuggestion(string query, string city = "全国");

//        #endregion

//        #region Place区域检索示例方法

//        Result<List<SuggestionResult>> Search(string query, string city = "成都市");

//        #endregion
//    }

//    /// <summary>
//    /// 百度地图API服务
//    /// </summary>
//    public class BaiDuMapCommonService : IBaiDuMapCommonService
//    {
//        private readonly IOptionsMonitor<AppSettings> _configuration;

//        public BaiDuMapCommonService(IOptionsMonitor<AppSettings> configuration)
//        {
//            _configuration = configuration;
//        }

//        #region private

//        private string MapHost => _configuration.CurrentValue.MapHost;

//        /// <summary>
//        /// 驾车距离查询接口地址
//        /// </summary>
//        private string MapDrivingUrl => _configuration.CurrentValue.MapDrivingUrl;

//        /// <summary>
//        /// 地理编码查询接口地址
//        /// </summary>
//        private string MapGeocoderUrl => _configuration.CurrentValue.MapGeocoderUrl;

//        /// <summary>
//        /// 输入关键字辅助信息查询接口地址
//        /// </summary>
//        private string MapSuggestionUrl => _configuration.CurrentValue.MapSuggestionUrl;

//        /// <summary>
//        /// 应用的AK密钥
//        /// </summary>
//        private string MapAk => _configuration.CurrentValue.MapAk;

//        /// <summary>
//        /// 应用的sk私钥
//        /// </summary>
//        private string MapSk => _configuration.CurrentValue.MapSk;

//        private const string OutputString = "json";

//        #endregion

//        #region 检测config中是否配置了地图设置

//        private Result CheckMapKey()
//        {
//            if (MapHost.IsNullOrEmpty())
//                Result.InnerError("请在appsettings.json中配置MapHost");
//            if (MapDrivingUrl.IsNullOrEmpty())
//                Result.InnerError("请在appsettings.json中配置百度地图驾车距离查询接口地址->MapDrivingUrl");
//            if (MapGeocoderUrl.IsNullOrEmpty())
//                Result.InnerError("请在appsettings.json中配置百度地图地理编码查询接口地址->MapGeocoderUrl");
//            if (MapSuggestionUrl.IsNullOrEmpty())
//                Result.InnerError("请在appsettings.json中配置百度地图输入关键字辅助信息查询接口地址->MapSuggestionUrl");
//            if (MapAk.IsNullOrEmpty())
//                Result.InnerError("请在appsettings.json中配置百度地图应用的AK密钥->MapAk");
//            if (MapSk.IsNullOrEmpty())
//                Result.InnerError("请在appsettings.json中配置百度地图应用的SK私钥->MapSk");
//            return Result.Success();
//        }


//        #endregion

//        #region 驾车距离计算

//        /// <summary>
//        /// 根据坐标获取驾车距离
//        /// </summary>
//        /// <param name="originsLat">出发地纬度</param>
//        /// <param name="originsLng">出发地经度</param>
//        /// <param name="destinationsLat">目的地纬度</param>
//        /// <param name="destinationsLng">目的地经度</param>
//        /// <returns></returns>
//        public Result<MapDistance> GetDrivingDistance(string originsLat, string originsLng,
//            string destinationsLat, string destinationsLng)
//        {
//            var result = CheckMapKey();
//            if (!result.IsSuccess)
//                return Result<MapDistance>.InnerError(result.ErrorMessage);
//            var origins = $"{originsLat},{originsLng}";
//            var destinations = $"{destinationsLat},{destinationsLng}";
//            return GetDrivingDistance(origins, destinations);
//        }

//        /// <summary>
//        /// 根据坐标获取驾车距离
//        /// </summary>
//        /// <param name="origins">出发地标准坐标</param>
//        /// <param name="destinations">目的地标准坐标</param>
//        /// <returns></returns>
//        public Result<MapDistance> GetDrivingDistance(string origins, string destinations)
//        {
//            var checkMap = CheckMapKey();
//            if (!checkMap.IsSuccess)
//                return Result<MapDistance>.InnerError(checkMap.ErrorMessage);
//            var querystringArrays = new Dictionary<string, string>
//            {
//                {"ak", MapAk},
//                {"output", OutputString},
//                {"timestamp", AksnCaculater.GetTimeStamp()},
//                {"origins", origins},
//                {"destinations", destinations},
//                { "tactics","11"}//常规路线，即多数人常走的一条路线，不受路况影响，可用于用车估价；
//            };
//            querystringArrays.Add("sn", AksnCaculater.CaculateAKSN(MapAk, MapSk, MapDrivingUrl, querystringArrays));
//            var url = MapHost + MapDrivingUrl;
//            var result = HttpRequestHelper.SendRequest(url + "?" + AksnCaculater.HttpBuildQuery(querystringArrays));
//            if (!result.IsSuccess) return Result<MapDistance>.LogicError(result.ErrorMessage);
//            var drivingDistance = result.Data.JsonToObj<DrivingDistance>();
//            if (drivingDistance.Status == 0)
//            {
//                var firstOrDefault = drivingDistance.Result.FirstOrDefault();
//                return Result<MapDistance>.Success(firstOrDefault != null ? firstOrDefault.Distance : new MapDistance());
//            }
//            else
//                return Result<MapDistance>.LogicError(drivingDistance.Message);
//        }

//        #endregion

//        #region 地理编码查询

//        /// <summary>
//        /// 地理编码查询
//        /// </summary>
//        /// <param name="address">检索地址</param>
//        /// <param name="city">城市</param>
//        /// <returns></returns>
//        public Result<GeocoderResult> GetGeocoder(string address, string city = "北京市")
//        {
//            var checkMap = CheckMapKey();
//            if (!checkMap.IsSuccess)
//                return Result<GeocoderResult>.InnerError(checkMap.ErrorMessage);

//            SortedDictionary<string, string> querystringArrays
//                = new SortedDictionary<string, string>
//                {
//                    {"ak", MapAk},
//                    {"address", address},
//                    {"City", city},
//                    {"output", OutputString},
//                    {"timestamp", AksnCaculater.GetTimeStamp()}
//                };
//            querystringArrays.Add("sn", AksnCaculater.CaculateAKSN(MapAk, MapSk, MapGeocoderUrl, querystringArrays));
//            var url = MapHost + MapGeocoderUrl + "?" + AksnCaculater.HttpBuildQuery(querystringArrays);

//            var result = HttpRequestHelper.SendRequest(url);
//            if (!result.IsSuccess) return Result<GeocoderResult>.LogicError(result.ErrorMessage);
//            GeocoderModel geocoderModel = result.Data.JsonToObj<GeocoderModel>();
//            return geocoderModel.Status == 0
//                ? Result<GeocoderResult>.Success(geocoderModel.Result ?? new GeocoderResult())
//                : Result<GeocoderResult>.LogicError(geocoderModel.Message);
//        }

//        /// <summary>
//        /// 逆地理编码服务
//        /// </summary>
//        /// <param name="location">坐标地址</param>
//        /// <returns></returns>
//        public Result<ReverseGeocodingReuslt> GetGeocoder(string location)
//        {
//            var checkMap = CheckMapKey();
//            if (!checkMap.IsSuccess)
//                return Result<ReverseGeocodingReuslt>.InnerError(checkMap.ErrorMessage);
//            Dictionary<string, string> querystringArrays = new Dictionary<string, string>
//            {
//                {"ak", MapAk},
//                {"location", location},
//                {"output", OutputString},
//                {"timestamp", AksnCaculater.GetTimeStamp()}
//            };
//            querystringArrays.Add("sn", AksnCaculater.CaculateAKSN(MapAk, MapSk, MapGeocoderUrl, querystringArrays));
//            var url = MapHost + MapGeocoderUrl + "?" + AksnCaculater.HttpBuildQuery(querystringArrays);

//            var result = HttpRequestHelper.SendRequest(url);
//            if (!result.IsSuccess) return Result<ReverseGeocodingReuslt>.LogicError(result.ErrorMessage);
//            ReverseGeocoding reverseGeocoding = result.Data.JsonToObj<ReverseGeocoding>();
//            return reverseGeocoding.Status == 0 ? Result<ReverseGeocodingReuslt>.Success(reverseGeocoding.Result ?? new ReverseGeocodingReuslt()) : Result<ReverseGeocodingReuslt>.LogicError(reverseGeocoding.Message);
//        }
//        /// <summary>
//        /// 逆地理编码服务
//        /// </summary>
//        /// <param name="lat">纬度</param>
//        /// <param name="lng">经度</param>
//        /// <returns></returns>
//        public Result<ReverseGeocodingReuslt> GetGeocoderForLatAndLng(string lat, string lng)
//        {
//            var checkMap = CheckMapKey();
//            if (!checkMap.IsSuccess)
//                return Result<ReverseGeocodingReuslt>.InnerError(checkMap.ErrorMessage);
//            string location = $"{lat},{lng}";
//            return GetGeocoder(location);
//        }

//        #endregion

//        #region 输入关键字辅助信息查询

//        /// <summary>
//        /// 输入关键字辅助信息查询
//        /// </summary>
//        /// <param name="query">检索关键字</param>
//        /// <param name="city">城市</param>
//        /// <returns></returns>
//        public Result<List<SuggestionResult>> GetSuggestion(string query, string city = "全国")
//        {
//            var checkMap = CheckMapKey();
//            if (!checkMap.IsSuccess)
//                return Result<List<SuggestionResult>>.InnerError(checkMap.ErrorMessage);
//            IDictionary<string, string> querystringArrays = new Dictionary<string, string>();

//            querystringArrays.Add("ak", MapAk);
//            querystringArrays.Add("output", OutputString);
//            querystringArrays.Add("query", query);
//            querystringArrays.Add("region", city);
//            querystringArrays.Add("timestamp", AksnCaculater.GetTimeStamp());
//            //querystringArrays.Add("city_limit", "true");

//            querystringArrays.Add("sn", AksnCaculater.CaculateAKSN(MapAk, MapSk, MapSuggestionUrl, querystringArrays));
//            var url = AksnCaculater.HttpBuildQuery(querystringArrays);

//            var result = HttpRequestHelper.SendRequest(MapHost + MapSuggestionUrl + "?" + url);
//            if (!result.IsSuccess) return Result<List<SuggestionResult>>.LogicError(result.ErrorMessage);
//            SuggestionModel suggestionModel = result.Data.JsonToObj<SuggestionModel>();
//            return suggestionModel.Status == 0 ? Result<List<SuggestionResult>>.Success(suggestionModel.Results ?? new List<SuggestionResult>()) : Result<List<SuggestionResult>>.LogicError(suggestionModel.Message);
//        }

//        #endregion

//        #region Place区域检索示例方法

//        public Result<List<SuggestionResult>> Search(string query, string city = "成都市")
//        {
//            var checkMap = CheckMapKey();
//            if (!checkMap.IsSuccess)
//                return Result<List<SuggestionResult>>.InnerError(checkMap.ErrorMessage);
//            IDictionary<string, string> querystringArrays = new Dictionary<string, string>();

//            querystringArrays.Add("ak", MapAk);
//            querystringArrays.Add("output", OutputString);
//            querystringArrays.Add("query", query);
//            querystringArrays.Add("region", city);
//            //querystringArrays.Add("timestamp", AksnCaculater.GetTimeStamp());
//            querystringArrays.Add("city_limit", "true");

//            //querystringArrays.Add("sn", AksnCaculater.CaculateAKSN(MapAk, MapSk, MapSuggestionUrl, querystringArrays));
//            var url = AksnCaculater.HttpBuildQuery(querystringArrays);

//            var result = HttpRequestHelper.SendRequest(MapHost + "/place/v2/search" + "?" + url);
//            if (!result.IsSuccess) return Result<List<SuggestionResult>>.LogicError(result.ErrorMessage);
//            SuggestionModel suggestionModel = result.Data.JsonToObj<SuggestionModel>();
//            return suggestionModel.Status == 0 ? Result<List<SuggestionResult>>.Success(suggestionModel.Results ?? new List<SuggestionResult>()) : Result<List<SuggestionResult>>.LogicError(suggestionModel.Message);
//        }

//        #endregion
//    }

//    /// <summary>
//    /// 百度地图API sn计算算法
//    /// </summary>
//    public class AksnCaculater
//    {
//        private static string MD5(string password)
//        {
//            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(password);
//            try
//            {
//                System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
//                cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
//                byte[] hash = cryptHandler.ComputeHash(textBytes);
//                //原转换部分保留，可以debug对比
//                string ret = "";
//                foreach (byte a in hash)
//                {
//                    ret += a.ToString("x");
//                }
//                //百度官方算法有bug，hash值小于10，只占一位
//                var m5 = BitConverter.ToString(hash).Replace("-", "");
//                return m5.ToLower();//只接受小写的md5
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        private static string UrlEncode(string str)
//        {
//            str = System.Web.HttpUtility.UrlEncode(str, Encoding.UTF8);
//            byte[] buf = Encoding.ASCII.GetBytes(str);//等同于Encoding.ASCII.GetBytes(str)
//            for (int i = 0; i < buf.Length; i++)
//                if (buf[i] == '%')
//                {
//                    if (buf[i + 1] >= 'a') buf[i + 1] -= 32;
//                    if (buf[i + 2] >= 'a') buf[i + 2] -= 32;
//                    i += 2;
//                }
//            return Encoding.ASCII.GetString(buf);//同上，等同于Encoding.ASCII.GetString(buf)
//        }

//        public static string HttpBuildQuery(IDictionary<string, string> querystring_arrays)
//        {

//            StringBuilder sb = new StringBuilder();
//            foreach (var item in querystring_arrays)
//            {
//                sb.Append(UrlEncode(item.Key));
//                sb.Append("=");

//                if (item.Value.Contains(","))
//                {
//                    var valueArr = item.Value.Split(',');
//                    if (valueArr.Length > 0)
//                    {
//                        foreach (var valueStr in valueArr)
//                        {
//                            sb.Append(UrlEncode(valueStr));
//                            sb.Append(",");
//                        }
//                        sb.Remove(sb.Length - 1, 1);
//                    }
//                }
//                else
//                {
//                    sb.Append(UrlEncode(item.Value));
//                }
//                sb.Append("&");
//            }
//            sb.Remove(sb.Length - 1, 1);
//            return sb.ToString();
//        }

//        public static string CaculateAKSN(string ak, string sk, string url, IDictionary<string, string> querystring_arrays)
//        {
//            var queryString = HttpBuildQuery(querystring_arrays);

//            var str = UrlEncode(url + "?" + queryString + sk);

//            return MD5(str);
//        }

//        /// <summary>  
//        /// 获取时间戳  
//        /// </summary>  
//        /// <returns></returns>  
//        public static string GetTimeStamp()
//        {
//            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
//            return Convert.ToInt64(ts.TotalSeconds).ToString();
//        }
//    }

//}
