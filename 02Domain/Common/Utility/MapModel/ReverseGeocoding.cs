namespace JSHC.OrderingSystem.Common.Utility.MapModel
{
    /// <summary>
    ///  逆地理编码
    /// </summary>
    public class ReverseGeocoding : MapBaseModel
    {
        public ReverseGeocodingReuslt Result { get; set; }
    }
    public class ReverseGeocodingReuslt 
    {
        /// <summary>
        /// 当前位置结合POI的语义化结果描述。
        /// </summary>
        public string Sematic_description { get; set; }
        /// <summary>
        /// 城市id
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 经纬度坐标
        /// </summary>
        public MapLocation Location { get; set; }
        /// <summary>
        /// 结构化地址信息
        /// </summary>
        public string formatted_address { get; set; }
        /// <summary>
        /// 所在商圈信息，如 "人民大学,中关村,苏州街"
        /// </summary>
        public string Business { get; set; }

        public MapAddressComponent AddressComponent { get; set; }
    }

    public class MapAddressComponent
    {
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 国家代码
        /// </summary>
        public string country_code { get; set; }
        /// <summary>
        /// 省名
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市名
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区县名
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 行政区划代码
        /// </summary>
        public string Adcode { get; set; }
        /// <summary>
        /// 街道名
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// 街道门牌号
        /// </summary>
        public string StreetNumber { get; set; }
        /// <summary>
        /// 和当前坐标点的方向，当有门牌号的时候返回数据
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// 和当前坐标点的距离，当有门牌号的时候返回数据
        /// </summary>
        public string Distance { get; set; }
    }
}
