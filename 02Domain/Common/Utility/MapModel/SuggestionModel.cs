using System.Collections.Generic;

namespace JSHC.OrderingSystem.Common.Utility.MapModel
{
    /// <summary>
    /// 匹配用户输入关键字辅助信息
    /// </summary>
    public class SuggestionModel : MapBaseModel
    {
        /// <summary>
        /// 建议词条列表
        /// </summary>
        public List<SuggestionResult> Results { get; set; }
    }
    /// <summary>
    /// 建议词条列表
    /// </summary>
    public class SuggestionResult
    {
        /// <summary>
        /// poi名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// poi经纬度坐标
        /// </summary>
        public MapLocation Location { get; set; }

        /// <summary>
        /// poi的唯一标示，ID
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区县
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 地址信息
        /// </summary>
        public string Address { get; set; }
    }
}
