using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WechatClockWinServer
{
    // https://developers.dingtalk.com/document/app/custom-robot-access
    public class DingBotNotification
    {
        private const string DingBotWebHookUrl =
            "https://oapi.dingtalk.com/robot/send?access_token=";

        public static async Task MainTest()
        {
            var data = new { msgtype = "text", text = new { content = $"Test... {DateTime.Now:yyyy-MM-dd HH:mm:ss}" } };
            using var httpClient = new HttpClient();
            using var response = await httpClient.PostAsync(DingBotWebHookUrl, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"发送钉钉消息通知，result:{result}");
        }
    }
}
