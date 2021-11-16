using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class VisitVisitReport
    {
        public int IdReport{ get; set; }
        public Status StatusReport{ get; set; }

        public int IdVisit { get; set; }
        public Status StatusVisit { get; set; }
        public int VersionVisit { get; set; }
        public int NameVisit { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int VersionReport{ get; set; }
        public int Version { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
        public virtual Visit Visit { get; set; }
        public virtual VisitReport Report{ get; set; }

        
    }
}
