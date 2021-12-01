using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Objection 
    {
        public int IdObjection { get; set; }
        public int Active { get; set; }
        public int Version { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public Status Status { get; set; }
        public int? Satisfaction { get; set; }
        public StatusSatisfaction? StatusSatisfaction { get; set; }

        public StatusCompletion StatusCompletion { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? IdDoctor { get; set; }
        public int? VersionDoctor { get; set; }
        public Status? StatusDoctor { get; set; }

   
        public virtual Doctor Doctor { get; set; }
        public int IdUser { get; set; }
        public int VersionUser { get; set; }
        public Status StatusUser { get; set; }
        public virtual User User { get; set; }
        public string CreatedByName { get; set; }
        public string UpdatedByName { get; set; }

        public int? IdPharmacy{ get; set; }
        public int? VersionPharmacy{ get; set; }
        public Status? StatusPharmacy{ get; set; }


        public virtual Pharmacy Pharmacy{ get; set; }
        public bool OnProduct { get; set; }

        public int? IdProduct { get; set; }
        public int? VersionProduct { get; set; }
        public Status? StatusProduct { get; set; }


        public virtual Product Product { get; set; }
        public string Response { get; set; }

    }

}
