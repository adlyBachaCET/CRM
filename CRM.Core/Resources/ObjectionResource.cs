using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class ObjectionResource
    {
        public int IdObjection { get; set; }
        public int Active { get; set; }
        public int Version { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public Status Status { get; set; }


        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? IdDoctor { get; set; }
        public int? VersionDoctor { get; set; }
        public Status? StatusDoctor { get; set; }


        public virtual DoctorResource Doctor { get; set; }
        public int IdUser { get; set; }
        public int VersionUser { get; set; }
        public Status StatusUser { get; set; }
        public virtual UserResource User { get; set; }

        public int? IdPharmacy { get; set; }
        public int? VersionPharmacy { get; set; }
        public Status? StatusPharmacy { get; set; }


        public virtual PharmacyResource Pharmacy { get; set; }
        public bool OnProduct { get; set; }

        public int? IdProduct { get; set; }
        public int? VersionProduct { get; set; }
        public Status? StatusProduct { get; set; }
        public string CreatedByName { get; set; }
        public string UpdatedByName { get; set; }

        public virtual ProductResource Product { get; set; }
        public string Response { get; set; }

        public int? Satisfaction { get; set; }

    }

}
