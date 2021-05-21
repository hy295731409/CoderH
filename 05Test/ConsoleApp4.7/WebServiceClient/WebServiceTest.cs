using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._7.WebServiceClient
{
    /// <summary>
    /// 调用webservice的三种方式
    /// </summary>
    public class WebServiceTest
    {
        public static void Test()
        {
            //通过添加服务引用调用webservice
            var ws = new WebReference.WebService();
            var res = ws.HelloWorld("hxp", 234);
            Console.WriteLine(res);

            //通过wsdl.exe + wsdl文件自动生成代理类
            var ws2 = new WebServiceAgent.WebService.WebService();
            var res2 = ws2.HelloWorld("zzz", 333);
            Console.WriteLine(res2);

            var url = "http://172.18.5.220:8020/WebService.asmx";
            var sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            sb.Append("<soap:Body>");
            sb.Append("<HelloWorld xmlns=\"http://tempuri.org/\">");
            sb.Append("<str>httpclient</str>");
            sb.Append("<id>666</id>");
            sb.Append("</HelloWorld>");
            sb.Append("</soap:Body>");
            sb.Append("</soap:Envelope>");
            var _content = Encoding.UTF8.GetBytes(sb.ToString());
            MemoryStream ms = new MemoryStream(_content);
            var content = new StreamContent(ms);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
            var res3 = string.Empty;
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, content).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    res3 = response.Content.ReadAsStringAsync().Result;
                }
            }
            ms.Close();
            
            Console.WriteLine(res3);
        }
    }
}
