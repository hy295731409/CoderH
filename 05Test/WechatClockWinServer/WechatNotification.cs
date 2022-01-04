using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WechatClockWinServer
{
    public class WechatNotification
    {
        private const string CorpId = "ww3128b81a490251b6";
        private const string AppId = "1000002";
        private const string AppSecret = "kT1JLQgfFGoYV7xDB1UKnjBceeVhxvAGxpe0gR2jHQI";
        private const string ToUid = "@all";
        private static readonly TimeSpan morning = new TimeSpan(8, 55, 0);
        private static readonly TimeSpan afternoon = new TimeSpan(17, 29, 0);
        private static HttpClient client = new HttpClient();
        private Timer timer = new Timer();

        public static void CheckTime()
        {
            var now = DateTime.Now;
            var weekDay = now.DayOfWeek;
            var iNow = (int)now.TimeOfDay.TotalMinutes;
            if (weekDay != DayOfWeek.Saturday && weekDay != DayOfWeek.Sunday && (iNow == (int)morning.TotalMinutes || iNow == (int)afternoon.TotalMinutes))
                MainTest();
        }

        public static void MainTest()
        {
            try
            {
                var getTokenUrl = $"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={CorpId}&corpsecret={AppSecret}";
                var responseStr = client.GetStringAsync(getTokenUrl).Result;
                var jObj = JObject.Parse(responseStr);
                var accessToken = jObj.Value<string>("access_token");
                
                var sendMessageUrl = $"https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={accessToken}";
                //var data = new
                //{
                //    touser = ToUid,
                //    msgtype = "text",
                //    agentid = AppId,
                //    text = new { content = $"准备打卡 over！！！ {DateTime.Now:yyyy-MM-dd HH:mm:ss}" }
                //};
                //var str = JsonConvert.SerializeObject(data);

                var str = "{\"touser\":\"@all\",\"msgtype\":\"text\",\"agentid\":\"1000002\",\"text\":{\"content\":\"准备打卡 over！！！ "+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"\"}}";
                
                var buffer = Encoding.UTF8.GetBytes(str);
                var ms = new MemoryStream(buffer);
                var content = new StreamContent(ms);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = client.PostAsync(sendMessageUrl, content).Result;
                responseStr = response.Content.ReadAsStringAsync().Result;
                ms.Dispose();
                content.Dispose();
                //Console.WriteLine(responseStr);
            }
            catch (Exception e)
            {
                var datapath = Path.Combine( AppDomain.CurrentDomain.BaseDirectory ,"./log.text");
                File.AppendAllText(datapath, $"err:{e.Message}");
            }
        }


        public void Start()
        {
            timer.Interval = 60 * 1000;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += (sender, e) => CheckTime();
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
