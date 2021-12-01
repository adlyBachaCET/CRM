using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SellingObjectives
    {
        public int IdSellingObjectives { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }

        public int? IdPharmacy { get; set; }
        public int? VersionPharmacy { get; set; }
        public Status? StatusPharmacy { get; set; }
        public virtual Pharmacy Pharmacy { get; set; }
        public int? IdDoctor { get; set; }
        public int? VersionDoctor { get; set; }
        public Status? StatusDoctor { get; set; }
        public virtual Doctor Doctor { get; set; }

        public int? IdUser { get; set; }
        public int VersionUser { get; set; }
        public Status StatusUser { get; set; }
        public virtual User User { get; set; }

        public int IdProduct { get; set; }
        public Status StatusProduct { get; set; }
        public int VersionProduct { get; set; }
        public virtual Product IdProductNavigation { get; set; }
        public int Quantity { get; set; }
    }
}
