using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Medicom.PASSPA2CollectService
{
    public class Helper
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Helper));
        public static string ReturnCreateSql(string tableName, object entity)
        {
            try
            {
                PropertyInfo[] pInfos = entity.GetType().GetProperties();
                var strSql = new StringBuilder();
                string cloumName = "";
                string paraName = "";
                foreach (PropertyInfo pInfo in pInfos)
                {
                    cloumName += pInfo.Name + ",";
                    paraName += "@" + pInfo.Name + ",";
                }
                strSql.Append("insert into " + tableName + "(");
                strSql.Append(cloumName.TrimEnd(',') + ")");
                strSql.Append(" values (");
                strSql.Append(paraName.TrimEnd(',') + ")");
                return strSql.ToString();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("初始化{0}表SQL语句时出错，信息：{1}。", tableName, ex.Message);
                return string.Empty;
            }
        }
        public static string ReturnUpdateSql(string tableName, object entity)
        {
            try
            {
                PropertyInfo[] pInfos = entity.GetType().GetProperties();
                var strSql = new StringBuilder();
                string cloumName = "";
                foreach (PropertyInfo pInfo in pInfos)
                {
                    cloumName += pInfo.Name + "=" + "@" + pInfo.Name + ",";
                }
                strSql.Append("update " + tableName + " set ");
                strSql.Append(cloumName.TrimEnd(','));
                return strSql.ToString();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("初始化{0}表SQL语句时出错，信息：{1}。", tableName, ex.Message);
                return string.Empty;
            }
        }
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            try
            {
                DataTable dtReturn = new DataTable();
                PropertyInfo[] oProps = null;
                foreach (T rec in list)
                {
                    if (oProps == null)
                    {
                        oProps = ((Type)rec.GetType()).GetProperties();
                        foreach (PropertyInfo pi in oProps)
                        {
                            Type colType = pi.PropertyType; if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                            {
                                colType = colType.GetGenericArguments()[0];
                            }
                            dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                        }
                    }
                    DataRow dr = dtReturn.NewRow(); foreach (PropertyInfo pi in oProps)
                    {
                        dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
                    }

                    dtReturn.Rows.Add(dr);
                }
                return dtReturn;
            }
            catch
            {
                return null;
            }
        }
        public static List<string> ReturnEntityProperties(object entity)
        {
            PropertyInfo[] pInfos = entity.GetType().GetProperties();
            var list = new List<string>();
            foreach (PropertyInfo pInfo in pInfos)
            {
                list.Add(pInfo.Name);
            }
            return list;
        }

        public static string Date(string date)
        {
            try
            {
                return DateTime.ParseExact(date, "yyyyMMddhhmmss", null).ToString("yyyy-MM-dd");
            }
            catch
            {
                return "1900-01-01";
            }

        }
        public static int Age(string value)
        {
            try
            {
                var date = Date(value);
                var bir = DateTime.ParseExact(date, "yyyy-MM-dd", null);
                var now = DateTime.Now;
                if (bir.Month < now.Month || (bir.Month == now.Month && bir.Day >= now.Day))
                    return now.Year - bir.Year;
                else
                    return now.Year - bir.Year - 1;
            }
            catch
            {
                return 0;
            }
        }
        public static Message ReturnEntity(string message)
        {
            var entity = new Message();
            entity.Content = message.Split(Environment.NewLine.ToCharArray()).ToList();
            var paragraph_MSH = entity.Content.Find(e => e.Contains("MSH|"));
            var MessageArray_MSH = paragraph_MSH.Split('|');
            entity.MessageType = MessageArray_MSH.Count() > 8 ? MessageArray_MSH[8] : "";
            entity.MessageId = MessageArray_MSH.Count() > 9 ? MessageArray_MSH[9] : "";
            return entity;
        }
    }
    public class OrcAndItem
    {
        public string Orc;//处方   
        public List<string> NTE_List = new List<string>();
        public List<string> OBR_List = new List<string>();
        public List<string> RXA_List = new List<string>();
        public List<string> RXO_List = new List<string>();
        public List<string> RXR_List = new List<string>();
        public List<string> FT1_List = new List<string>();
    }

    public class ObrAndItem
    {
        public string Obr;//检验 检查
        public List<string> OBX_List = new List<string>();
        public List<string> NET_List = new List<string>();
    }
}
