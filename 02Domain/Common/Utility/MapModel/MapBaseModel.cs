namespace JSHC.OrderingSystem.Common.Utility.MapModel
{
    /// <summary>
    /// 百度API接口返回基础数据
    /// </summary>
    public class MapBaseModel
    {
        /// <summary>
        /// 状态码
        /// 0：成功
        /// 1：服务器内部错误
        /// 2：参数错误
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 响应信息
        /// 对status的中文描述
        /// </summary>
        public string Message { get; set; }
    }
}
