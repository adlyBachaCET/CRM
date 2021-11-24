using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Target 
    {
        public string NomTarget { get; set; }
        public int NumTarget { get; set; }

        public int IdCycle { get; set; }
        public Status StatusCycle { get; set; }
        public int VersionCycle { get; set; }      
        public virtual Cycle IdCycleNavigation { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdUser { get; set; }
        public Status StatusUser { get; set; }
        public int VersionUser { get; set; }
        public virtual User IdUserNavigation { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? IdDoctor { get; set; }
        public Status? StatusDoctor { get; set; }
        public int? VersionDoctor { get; set; }
        public virtual Doctor IdDoctorNavigation { get; set; }

        public int? IdPharmacy { get; set; }
        public Status? StatusPharmacy { get; set; }
        public int? VersionPharmacy { get; set; }
        public virtual Pharmacy IdPharmacyNavigation { get; set; }
        public int? IdSector { get; set; }

        public Status StatusSector { get; set; }
        public int VersionSector { get; set; }

        public virtual Sector IdSectorNavigation { get; set; }


        public Status Status { get; set; }
        public int Version { get; set; }
        public string Note { get; set; }
        public int NumSystemBrick { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
   
    }
}
