using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Object.Auth
{
    public class RefreshToken
    {
        public string Token { get; set; }
        /// <summary>
        /// 自定义的固定值
        /// </summary>
        public string refreshToken { get; set; }
    }
}
