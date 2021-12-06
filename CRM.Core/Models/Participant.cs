using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Participant
    {
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int? IdRequestRp{ get; set; }
        public Status? StatusRequestRp{ get; set; }
        public int? VersionRequestRp{ get; set; }
        public virtual RequestRp IdRequestRpNavigation { get; set; }
        public int? IdVisitReport { get; set; }
        public Status? StatusVisitReport { get; set; }
        public int? VersionVisitReport { get; set; }
        public virtual VisitReport IdVisitReportNavigation { get; set; }
        public int? IdDoctor { get; set; }
        public Status? StatusDoctor { get; set; }
        public int? VersionDoctor { get; set; }
        public virtual Doctor IdDoctorNavigation { get; set; }

        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public Status Status { get; set; }
        public StatusParticipant Context { get; set; }

        public int? IdUser { get; set; }
        public Status? StatusUser { get; set; }

        public int? VersionUser { get; set; }
        public virtual User IdUserNavigation { get; set; }
        public int? IdPharmacy { get; set; }
        public Status? StatusPharmacy { get; set; }
        public int? VersionPharmacy { get; set; }
        public virtual Pharmacy IdPharmacyNavigation { get; set; }
    }

}
