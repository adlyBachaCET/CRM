using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class VisitFileTracking
    {
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int IdVisit{ get; set; }
        public Status StatusVisit{ get; set; }
        public int VersionVisit{ get; set; }

        public int IdFile { get; set; }
        public Status StatusFile { get; set; }
        public int VersionFile { get; set; }
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public Status Status { get; set; }
        public virtual File IdFileNavigation { get; set; }
        public virtual Visit IdVisitNavigation { get; set; }
        public int IdTracking { get; set; }
        public Status StatusTracking { get; set; }

        public int VersionTracking { get; set; }
        public virtual Tracking IdTrackingNavigation { get; set; }
   

    }
}
