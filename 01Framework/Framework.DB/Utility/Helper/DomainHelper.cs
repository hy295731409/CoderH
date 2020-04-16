using System.Configuration;

namespace JSHC.IFramework.Utility.Helper
{
    public static class DomainHelper
    {
        #region Domain

        public static string GetDomain()
        {
            return ConfigurationManager.AppSettings["Domain"];
        }

        #endregion

        #region EbDomain

        public static string GetEBDomain()
        {
            return ConfigurationManager.AppSettings["EBDomain"];
        }

        #endregion

        #region O2ODomain

        public static string GetO2ODomain()
        {
            return ConfigurationManager.AppSettings["O2ODomain"];
        }

        #endregion

        #region TMSDomain

        public static string GetTMSDomain()
        {
            return ConfigurationManager.AppSettings["TMSDomain"];
        }

        #endregion

        #region SSODomain

        public static string GetSSODomain()
        {
            return ConfigurationManager.AppSettings["SSODomain"];
        }

        #endregion

        #region PSDomain

        public static string GetPSDomain()
        {
            return ConfigurationManager.AppSettings["PSDomain"];
        }

        #endregion

        #region PAYDomain

        public static string GetPAYDomain()
        {
            return ConfigurationManager.AppSettings["PAYDomain"];
        }

        #endregion
    }
}
