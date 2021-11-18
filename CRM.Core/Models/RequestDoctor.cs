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
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int IdDoctor { get; set; }
        public int VersionDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public virtual Doctor Doctor { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int IdUser{ get; set; }
        public int VersionUser { get; set; }
        public Status StatusUser { get; set; }
        public virtual User User { get; set; }



    }
}
