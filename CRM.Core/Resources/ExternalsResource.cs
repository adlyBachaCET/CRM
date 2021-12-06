using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class ExternalsResource
    {
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdExternal { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

    }
}
