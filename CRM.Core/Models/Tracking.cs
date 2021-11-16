using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class Tracking
    {
        public int IdTracking { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string TimeElapsed { get; set; }
        public DateTime Date { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public virtual ICollection<VisitFileTracking> VisitFileTracking { get; set; }


    }
}
