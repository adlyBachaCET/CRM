using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class RequestDoctor
    {
        public int IdRequestDoctor { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int IdDoctor { get; set; }
        public int VersionDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public virtual Doctor Doctor { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }



    }
}
