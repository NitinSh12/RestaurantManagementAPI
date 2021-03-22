using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationAuthorizationDomain.Models
{
    public class ApplicationUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SecurityStamp { get; set; }
        public string Email { get; set; }
    }
}
