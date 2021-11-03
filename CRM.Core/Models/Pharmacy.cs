using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Pharmacy 
    {
        public Pharmacy()
        {
            PharmacyLocality = new HashSet<PharmacyLocality>();
            Phone = new HashSet<Phone>();
        }

        public int IdPharmacy { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public string FirstNameOwner { get; set; }
        public string LastNameOwner { get; set; }
        public string Seller { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<PharmacyLocality> PharmacyLocality { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
    }
}
