using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class TargetResource
    {
        public string NomTarget { get; set; }
        public int NumTarget { get; set; }

   
        public int? IdDoctor { get; set; }
        public Status? StatusDoctor { get; set; }
        public int? VersionDoctor { get; set; }
        public virtual DoctorResource IdDoctorNavigation { get; set; }

        public int? IdPharmacy { get; set; }
        public Status? StatusPharmacy { get; set; }
        public int? VersionPharmacy { get; set; }
        public virtual PharmacyResource IdPharmacyNavigation { get; set; }

        public Status Status { get; set; }
        public int Version { get; set; }
        public string Note { get; set; }
        public int NumSystemBrick { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }


    }
}
