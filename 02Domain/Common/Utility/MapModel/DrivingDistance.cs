using System.Collections.Generic;

namespace JSHC.OrderingSystem.Common.Utility.MapModel
{
    /// <summary>
    /// 线路距离
    /// </summary>
    public class DrivingDistance : MapBaseModel
    {
        /// <summary>
        /// 返回的结果
        /// </summary>
        public List<MapResult> Result { get; set; }
    }

    public class MapResult
    {
        /// <summary>
        /// 线路距离
        /// </summary>
        public MapDistance Distance { get; set; }
    }

    /// <summary>
    /// 线路距离
    /// </summary>
    public class MapDistance
    {
        /// <summary>
        /// 线路距离的文本描述
        /// 文本描述的单位有米、公里两种
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 线路距离的数值
        /// 数值的单位为米。若没有计算结果，值为0
        /// </summary>
        public decimal Value { get; set; } = 0;
    }
}
