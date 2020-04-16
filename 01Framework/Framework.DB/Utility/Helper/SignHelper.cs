#region JSHC
/**************************************************************** 
 * 作    者：JSHC_LIUFAJIN 
 * 创建时间：2018/3/7 15:22:17 
 * 当前版本：1.0.0.1 
 *  
 * 描述说明： 
 * 
 * 修改历史： 
 * 
***************************************************************** 
 * Copyright @JSHC 2018 All rights reserved 
*****************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSHC.IFramework.Encryption;

namespace JSHC.IFramework.Utility.Helper
{
    public class SignHelper
    {
        private SignHelper()
        {
        }

        public static SignHelper Instance()
        {
            return new SignHelper();
        }

        /// <summary>
        /// 根据POST参数对象获取
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public string GetSign(Dictionary<string, object> postData, string privateKey = "", string privateValue = "")
        {
            if (postData == null)
                return String.Empty;

            var sortDic = new SortedDictionary<string, object>();
            foreach (var keyValuePair in postData)
            {
                sortDic.Add(keyValuePair.Key, keyValuePair.Value);
            }
            if (!privateKey.IsNullOrEmpty() && !privateValue.IsNullOrEmpty())
                sortDic.Add(privateKey, privateValue);
            var str = string.Join("&", sortDic.Select(u => u.Key + "=" + u.Value));
            var signature = EncryptionFactory.Md5Encrypt(str);
            return signature;
        }
    }
}
