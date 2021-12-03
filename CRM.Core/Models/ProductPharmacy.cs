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
        public string Name{ get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public virtual Pharmacy IdPharmacyNavigation { get; set; }
        public virtual Product Product { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int Quantity { get; set; }
       
        



    }
}
