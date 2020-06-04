using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Object.Auth
{
    public class RequestTokenInput
    {
        public string Name { get; set; }
        public string Pwd { get; set; }
    }
}
