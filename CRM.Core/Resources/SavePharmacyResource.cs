using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SavePharmacyResource
    {
        public string Name { get; set; }
        public string Role { get; set; }//Pharmacy ou grossicte
        public int IdLocality1 { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string NameLocality1 { get; set; }
        public int IdLocality2 { get; set; }

        public string NameLocality2 { get; set; }

        public int IdLocality3 { get; set; }
        public string NameLocality3 { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }
        public int IdPotentiel { get; set; }

        public string NamePotentiel { get; set; }

        public string FirstNameOwner { get; set; }
        public string LastNameOwner { get; set; }
        public string Email { get; set; }

    }
}
