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
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public PhoneType PhoneType { get; set; }
        public int PhoneNumber { get; set; }

        public string PhoneInfo { get; set; }
        public int? IdDoctor { get; set; }
        public int VersionDoctor { get; set; }
        public Status StatusDoctor { get; set; }
    
        public int? IdPharmacy { get; set; }
        public int VersionPharmacy { get; set; }
        public Status StatusPharmacy { get; set; }
   
        public Status Status { get; set; }
     

    }
 
}
