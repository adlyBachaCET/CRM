using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveAddPharmacyResource
    {
        public SavePharmacyResource SavePharmacyResource { get;set;}
        public List<SavePhoneResource> SavePhoneResource { get; set; }

    }


}
