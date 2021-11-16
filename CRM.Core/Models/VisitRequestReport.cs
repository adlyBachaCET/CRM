using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class VisitRequestReport
    {
        public int IdReport{ get; set; }
        public Status StatusReport{ get; set; }

        public int IdVisitRequest{ get; set; }
        public Status StatusVisitRequest{ get; set; }
        public int VersionVisitRequest{ get; set; }
        public int NameVisitRequest{ get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int VersionReport{ get; set; }
        public int Version { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
        public virtual VisitRequest VisitRequest{ get; set; }
        public virtual VisitReport Report{ get; set; }
    }
}
