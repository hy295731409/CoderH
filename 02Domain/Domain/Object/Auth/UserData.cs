using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Object.Auth
{
    public class UserData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public string Role { get; set; }
    }
}
