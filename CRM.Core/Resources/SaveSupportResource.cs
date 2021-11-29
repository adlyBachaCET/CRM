using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SaveSupportResource
    {


        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
    public class SendMail
    {
        public string Name { get; set; }
        public string EmailLogin { get; set; }

    }
    public class SendMailHtml
    {
        public string Name { get; set; }
        public string EmailLogin { get; set; }
        public string Html { get; set; }


    }
}
