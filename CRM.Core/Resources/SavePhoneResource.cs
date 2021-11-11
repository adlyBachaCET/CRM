using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SavePhoneResource 
    {
        public int Active { get; set; }
        public int Version { get; set; }
        public string Description { get; set; }


        public PhoneType PhoneType { get; set; }
        public string PhoneInfo { get; set; }
        public int PhoneNumber { get; set; }

        public int? IdDoctor { get; set; }

        public int? IdUser { get; set; }
 
        public int? IdPharmacy { get; set; }

        public int? IdWholeSaler { get; set; }

        public Status Status { get; set; }


    }

}
