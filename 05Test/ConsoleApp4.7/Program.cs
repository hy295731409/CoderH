using ConsoleApp4._7.TaskDemo;
using ConsoleApp4._7.test;
using ConsoleApp4._7.WebServiceClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp4._7
{
    class Program
    {
        static void Main(string[] args)
        {
            //TaskClass.GetTestRes();
            //Console.WriteLine("flag");
            //Console.WriteLine($"AthreadId=" + Thread.CurrentThread.ManagedThreadId);
            //Demo1.test();

            //Console.WriteLine($"AthreadId=" + Thread.CurrentThread.ManagedThreadId);
            //Console.ReadKey();

            //WebServiceTest.Test();

            //var test = new Test();
            //test.GetTest();

            //var demo = new XPath();
            //demo.demo();

            GetData();

            Console.ReadKey();
        }
        public static string GetData(params object[] param)
        {
            string result = "";
            var Description = "科室字典";
            var timer = new Stopwatch();//计时器
            timer.Start();
            var queueList = new List<object>();
            try
            {
                var dataStr = getData();
                var doc = new XmlDocument();
                doc.LoadXml(dataStr);
               
                var dataNodes = doc.SelectNodes("/Return/Data/V_PAV_DICTDEPT");
                foreach (XmlNode item in dataNodes)
                {
                    var entity = new T_MC_DICT_DEPT();
                    entity.hiscode = item.SelectSingleNode("HISCODE").InnerText;
                    entity.deptcode = item.SelectSingleNode("DEPTCODE").InnerText;
                    entity.deptname = item.SelectSingleNode("DEPTNAME").InnerText;
                    //entity.is_clinic = Tools.ToInt(item.SelectSingleNode("/IS_CLINIC").InnerText);
                    //entity.is_inhosp = Tools.ToInt(item.SelectSingleNode("/IS_INHOSP").InnerText);
                    //entity.is_emergency = Tools.ToInt(item.SelectSingleNode("/IS_EMERGENCY").InnerText);
                    queueList.Add(entity);
                }
                //result = Tools.List2Xml(queueList);
                //_logger.DebugFormat("获取{0}耗时：{1}ms，返回结果：{2}", Description, timer.ElapsedMilliseconds, result);
            }
            catch (Exception ex)
            {
                //_logger.ErrorFormat("获取{0}出现错误：{1}", Description, ex.Message);
            }
            return result;
        }

        private static string getData()
        {
            var url = "http://192.168.172.185:8888/InterFace/InterFace_PAV/PAV?wsdl";
            var sb = new StringBuilder();
            sb.Append("<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:Server\">");
            sb.Append("<soapenv:Header/>");
            sb.Append("<soapenv:Body>");
            sb.Append("<urn:PAV_DictDept soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">");
            sb.Append("<UserInfo xsi:type=\"xsd: string\"><![CDATA[<UserInfo><ServiceIP>192.168.172.141</ServiceIP><App_Id>PAV_His_00001</App_Id><PassWord>PAV_His_00001</PassWord></UserInfo>]]></UserInfo>");
            sb.Append("<Token xsi:type=\"xsd: string\"><![CDATA[TmlzX0hpc18wMDAwMU5pc19IaXNfMDAwMDFOaXNfSGlzXzAwMDAxTmlzX0hpc18wMDAwMTE5Mi4xNjguMTcyLjE4NTE5Mi4xNjguMTcyLjE4NQ==]]></Token>");
            sb.Append("<EnterTheReference xsi:type=\"xsd: string\"><![CDATA[<EnterTheReference><ALL>0</ALL></EnterTheReference>]]></EnterTheReference>");
            sb.Append("</urn:PAV_DictDept>");
            sb.Append("</soapenv:Body>");
            sb.Append("</soapenv:Envelope>");
            var _content = Encoding.UTF8.GetBytes(sb.ToString());
            MemoryStream ms = new MemoryStream(_content);
            var content = new StreamContent(ms);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
            var returnStr = string.Empty;
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, content).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    returnStr = response.Content.ReadAsStringAsync().Result;
                }
            }
            ms.Close();
            content.Dispose();

            var res = string.Empty;
            var doc = new XmlDocument();
            doc.LoadXml(returnStr);
            var root = doc.DocumentElement;
            var nsMgr = new XmlNamespaceManager(doc.NameTable);
            nsMgr.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            nsMgr.AddNamespace("ns1", "urn:Server");
            //var resNode = doc.SelectSingleNode("/PAV_DictDept");
            var resNode = root.GetElementsByTagName("PAV_DictDept");
            if (resNode == null || string.IsNullOrEmpty(resNode[0].InnerText))
                return res;
            var resStr = resNode[0].InnerText;
            //<![CDATA[<Return><Result>1</Result><Err>记录为空</Err></Return>]]>
            doc.LoadXml(resStr);
            root = doc.DocumentElement;
            if (root.SelectSingleNode("Result").InnerText == "1")
            {
                //_logger.Error($"采集数据失败：{doc.SelectSingleNode("/Return/Err").InnerText}");
                return res;
            }
            return resStr;
        }
    }
    public class T_MC_DICT_DEPT
    {
        public T_MC_DICT_DEPT()
        {
            hiscode = "0";
            deptcode = "";
            deptname = "";
            is_clinic = -1;
            is_inhosp = -1;
            is_emergency = -1;
            is_save = 0;
        }
        public string hiscode { get; set; }
        public string deptcode { get; set; }
        public string deptname { get; set; }
        public string parentname { get; set; }
        public string searchcode { get; set; }
        public int? is_clinic { get; set; }
        public int? is_inhosp { get; set; }
        public int? is_emergency { get; set; }
        public int is_save { get; set; }
    }

}
