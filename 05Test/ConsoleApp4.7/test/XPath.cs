using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp4._7.test
{
    public class XPath
    {
        public string demo()
        {
            //var xmlPath = @"D:\Work\PASS2\PASSPA2\other\PASSPA2InterfaceAdapter\CODE-广东省中医院\文档\电子病历\首次病程记录.xml";
            //var xmlPath = @"D:\Work\PASS2\PASSPA2\other\PASSPA2InterfaceAdapter\CODE-广东省中医院\文档\电子病历\查房记录.xml";
            //var xmlPath = @"D:\Work\PASS2\PASSPA2\other\PASSPA2InterfaceAdapter\CODE-广东省中医院\文档\电子病历\抢救记录.xml";
            //var xmlPath = @"D:\Work\PASS2\PASSPA2\other\PASSPA2InterfaceAdapter\CODE-广东省中医院\文档\电子病历\会诊记录.xml";
            //var xmlPath = @"D:\Work\PASS2\PASSPA2\other\PASSPA2InterfaceAdapter\CODE-广东省中医院\文档\电子病历\死亡记录.xml";
            var xmlPath = @"D:\Work\PASS2\PASSPA2\other\PASSPA2InterfaceAdapter\CODE-广东省中医院\文档\电子病历\出院记录.xml";
            var xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            var root = xmldoc.DocumentElement;
            var nsMgr = new XmlNamespaceManager(xmldoc.NameTable);
            nsMgr.AddNamespace("ns", "urn:hl7-org:v3"); 
            //nsMgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance"); 

             var entity = new T_MR_INPAT_MEDICALRECORD();

            //time <effectiveTime value="20210721013424.0000"/>
            var node = root.GetElementsByTagName("effectiveTime").Item(0);
            if (node != null)
            {
                var val = node.Attributes.GetNamedItem("value").Value;
                if (!string.IsNullOrEmpty(val) && val.Length >= 14)
                    entity.time = $"{val.Substring(0, 4)}-{val.Substring(4, 2)}-{val.Substring(6, 2)} {val.Substring(8, 2)}:{val.Substring(10, 2)}:{val.Substring(12, 2)}";
            }
            //doctorname
            node = root.SelectSingleNode("//ns:legalAuthenticator/ns:assignedEntity/ns:assignedPerson/ns:name", nsMgr);
            if (node != null)
            {
                entity.doctorname = node.InnerText;
            }
            //recordtime
            //courseRec
            //node = xmldoc.SelectSingleNode("/ClinicalDocument/component/structuredBody/component/section[@displayName=\"病例特点\"]");
            #region 首次
            //var nodes = root.SelectNodes("//ns:component/ns:structuredBody/ns:component/ns:section", nsMgr);
            //if (nodes != null && nodes.Count > 0)
            //{
            //    var sb = new StringBuilder();
            //    foreach (XmlNode item in nodes)
            //    {
            //        //主诉
            //        if (item.SelectSingleNode("./ns:title", nsMgr).InnerText == "主诉")
            //            sb.AppendLine($"【主诉】{item.SelectSingleNode("./ns:text", nsMgr).InnerText}");
            //        //病例特点
            //        if (item.SelectSingleNode("./ns:title", nsMgr).InnerText == "病例特点")
            //            sb.AppendLine($"【病例特点】{item.SelectSingleNode("./ns:text", nsMgr).InnerText}");
            //        //初步中医诊断
            //        if (item.SelectSingleNode("./ns:title", nsMgr).InnerText == "中医诊断（初步诊断）")
            //            sb.AppendLine($"【中医诊断（初步诊断）】{item.SelectSingleNode("./ns:text", nsMgr).InnerText}");
            //        //初步西医诊断
            //        if (item.SelectSingleNode("./ns:title", nsMgr).InnerText == "西医诊断（初步诊断）")
            //            sb.AppendLine($"【西医诊断（初步诊断）】{item.SelectSingleNode("./ns:text", nsMgr).InnerText}");
            //    }
            //    entity.courseRec = sb.ToString();
            //} 
            #endregion
            var nodes = root.SelectNodes("//ns:component/ns:structuredBody/ns:component/ns:section", nsMgr);
            if (nodes != null && nodes.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (XmlNode item in nodes)
                {
                    var title = item.SelectSingleNode("./ns:title", nsMgr).InnerText;
                    var text = item.SelectSingleNode("./ns:text", nsMgr)?.InnerText;
                    ////查房记录头
                    //if (title == "查房记录头")
                    //{
                    //    var timeNode = item.SelectSingleNode("./ns:text/ns:content/ns:linkHtml[@styleCode=\"DateRange\"]", nsMgr);
                    //    if (timeNode != null)
                    //        sb.AppendLine($"【查房时间】{timeNode.FirstChild.InnerText}");
                    //    var roundtypeNode = item.SelectSingleNode("./ns:text/ns:content/ns:linkHtml[@styleCode=\"VocabularyCodeRange\"]", nsMgr);
                    //    if (roundtypeNode != null)
                    //        sb.AppendLine($"【查房类型】{roundtypeNode.FirstChild.InnerText}");
                    //    var doctorNode = item.SelectSingleNode("./ns:text/ns:content/ns:linkHtml[@styleCode=\"TextNode\"]", nsMgr);
                    //    if (doctorNode != null)
                    //        sb.AppendLine($"【查房医生】{doctorNode.FirstChild.InnerText}");
                    //}
                    ////内容
                    //if (title == "内容")
                    //    sb.AppendLine($"【查房内容】{text}");
                    //if (title == "抢救过程")
                    //    sb.AppendLine($"【抢救过程】{text}");
                    //if (title == "会诊意见及建议")
                    //    sb.AppendLine($"【会诊意见及建议】{text}");
                    //if (title == "诊疗经过（重点记录病情演变、抢救经过）")
                    //    sb.AppendLine($"【诊疗经过（重点记录病情演变、抢救经过）】{text}");

                    //诊疗经过
                    if (title == "诊疗经过")
                        sb.AppendLine($"【诊疗经过】{text}");
                    //手术情况
                    if (title == "手术情况新")
                        sb.AppendLine($"【手术情况】{text}");
                    //出院情况
                    if (title == "出院情况")
                        sb.AppendLine($"【出院情况】{text}");
                }
                entity.courseRec = sb.ToString();
                entity.contentorg = sb.ToString();
            }

            return "";
        }
    }


    public class T_MR_INPAT_MEDICALRECORD
    {
        public T_MR_INPAT_MEDICALRECORD()
        {
            mrid = "";
            time = "";
            courseRec = "";
            contentorg = "";
            doctorname = "";
            recordtime = "";
        }
        /// <summary>
        /// 病案号
        /// </summary>
        public string mrid { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string courseRec { get; set; }
        /// <summary>
        /// 原始正文
        /// </summary>
        public string contentorg { get; set; }
        /// <summary>
        /// 医生名称
        /// </summary>
        public string doctorname { get; set; }
        /// <summary>
        /// 入院记录文档时间
        /// </summary>
        public string recordtime { get; set; }
        /// <summary>
        /// 病程记录类型代码
        /// </summary>
        public string record_title_code { get; set; }
        /// <summary>
        /// 病程记录类型
        /// </summary>
        public string record_title { get; set; }
        /// <summary>
        /// 病案编号
        /// </summary>
        public string documentSetUuid { get; set; }

    }
}
