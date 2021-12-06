using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class Externals
    {
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdExternal { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }
        public int? IdRequestRp { get; set; }
        public Status? StatusRequestRp { get; set; }
        public int? VersionRequestRp { get; set; }
        public virtual RequestRp IdRequestRpNavigation { get; set; }
        public int? IdVisitReport { get; set; }
        public Status? StatusVisitReport { get; set; }
        public int? VersionVisitReport { get; set; }
        public virtual VisitReport IdVisitReportNavigation { get; set; }
        public StatusParticipant Context { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }


    }
}
