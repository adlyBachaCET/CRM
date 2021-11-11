using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class PhoneResource 
    {
        public int IdPhone { get; set; }
        public int Active { get; set; }
        public int Version { get; set; }
        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public PhoneType PhoneType { get; set; }
        public string PhoneInfo { get; set; }
        public int? IdDoctor { get; set; }
        public int VersionDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public int? IdUser { get; set; }
        public int VersionUser { get; set; }
        public Status StatusUser { get; set; }
        public int? IdPharmacy { get; set; }
        public int VersionPharmacy { get; set; }
        public Status StatusPharmacy { get; set; }
        public int? IdWholeSaler { get; set; }
        public int VersionWholeSaler { get; set; }
        public Status StatusWholeSaler { get; set; }
        public Status Status { get; set; }
        public virtual Doctor IdDoctorNavigation { get; set; }
        public virtual Pharmacy IdPharmacyNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
        public virtual WholeSaler IdWholeSalerNavigation { get; set; }

    }
 
}
