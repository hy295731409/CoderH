namespace JSHC.OrderingSystem.Common.Utility.MapModel
{
    /// <summary>
    /// 地理编码参数
    /// </summary>
    public class GeocoderModel : MapBaseModel
    {
        /// <summary>
        /// 查询结果
        /// </summary>
        public GeocoderResult Result { get; set; }
    }

    public class GeocoderResult
    {
        /// <summary>
        /// 经纬度坐标
        /// </summary>
        public MapLocation Location { get; set; }
    }
    public class MapLocation
    {
        /// <summary>
        /// 纬度值
        /// </summary>
        public decimal Lat { get; set; }
        /// <summary>
        /// 经度值
        /// </summary>
        public decimal Lng { get; set; }
    }
}
