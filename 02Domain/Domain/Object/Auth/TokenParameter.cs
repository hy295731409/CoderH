using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Object.Auth
{
    public class TokenParameter
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int AccessExpiration { get; set; }
        public int RefreshExpiration { get; set; }
    }
}
