using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class ProductPharmacy
    {
        public int IdProduct { get; set; }
        public Status StatusProduct { get; set; }

        public int IdPharmacy{ get; set; }
        public Status StatusPharmacy{ get; set; }
        public int Version { get; set; }
        public int VersionPharmacy{ get; set; }
        public int VersionProduct { get; set; }
        public int NamePharmacy{ get; set; }

        public virtual Pharmacy IdPharmacyNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int Quantity { get; set; }
       
        
        public int? IdDoctor { get; set; }
   
        public int VersionDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public virtual Doctor Doctor { get; set; }


    }
}
