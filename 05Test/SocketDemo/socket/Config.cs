using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Collections.Specialized;
using System.Web;

namespace Medicom.Common.DatabaseDriver
{
    public static class Config
    {
        /// <summary>
        /// 产品版本号在配置文件中的属性名
        /// </summary>
        public const string PRODUCT_VERSION = "product_version";

        //PASSCore接口
        public static string IP
        {
            get
            {
                string ip = System.Configuration.ConfigurationManager.AppSettings["IP"];
                if (ip != null)
                {
                    return ip;
                }
                else
                {
                    return "";
                }
            }
        }
        public static int PORT
        {
            get
            {
                string port = System.Configuration.ConfigurationManager.AppSettings["PORT"];
                if (!string.IsNullOrEmpty(port))
                {
                    return Convert.ToInt32(port);
                }
                else
                {
                    return 3900;
                }
            }
        }
        public static string ConnectString
        {
            get
            {
                string conStr = System.Configuration.ConfigurationManager.AppSettings["DATASOURCE"];
                if (!string.IsNullOrEmpty(conStr))
                {
                    return conStr;
                }
                else
                {
                    return "";
                }
            }
        }
        public static int THREADSIZE
        {
            get
            {
                string size = System.Configuration.ConfigurationManager.AppSettings["THREADSIZE"];
                if (!string.IsNullOrEmpty(size))
                {
                    return Convert.ToInt32(size);
                }
                else
                {
                    return 2;
                }
            }
        }
        public static string Provider
        {
            get
            {
                return "System.Data.SqlClient";
            }
        }
        public static int ConnTimeOut
        {
            get
            {
                return 30;
            }
        }
        public static int CmdTimeOut
        {
            get
            {
                return 3600;
            }
        }
    }
    public enum DataBaseType
    {
        /// <summary>
        /// 临床药学数据库
        /// </summary>
        Pa2Db
    }
}