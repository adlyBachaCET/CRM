using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class PharmacyLocality 
    {
        public int IdPharmacy { get; set; }
        public int VersionPharmacy { get; set; }
        public Status StatusPharmacy { get; set; }

        public int IdLocality { get; set; }
        public Status StatusLocality { get; set; }

        public int VersionLocality { get; set; }
        public int Version { get; set; }

        public virtual Locality IdLocalityNavigation { get; set; }
        public virtual Pharmacy IdPharmacyNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
    }
}
