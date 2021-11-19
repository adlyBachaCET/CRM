using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SupportResource
    {
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdSupport { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Status Status { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

    }
}
