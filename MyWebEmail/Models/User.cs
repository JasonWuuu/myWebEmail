using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebEmail.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class Setting
    {
        public string SmtpHostName { get; set; }
        public int SmtpPort { get; set; }

        public string Pop3HostName { get; set; }
        public int Pop3Port { get; set; }

        public User User { get; set; }
    }
}