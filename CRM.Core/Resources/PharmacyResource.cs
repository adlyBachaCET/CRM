using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class PharmacyResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int IdBrick1 { get; set; }
        public int IdBrick2 { get; set; }
        public string NameBrick1 { get; set; }
        public string NameBrick2 { get; set; }
        public int NumBrick1 { get; set; }
        public int NumBrick2 { get; set; }
        public GrossistePharmacy GrossistePharmacy { get; set; }

        public string Role { get; set; }//Pharmacy ou grossicte
        public Status Status { get; set; }
        public string NameLocality1 { get; set; }

        public int IdLocality2 { get; set; }

        public string NameLocality2 { get; set; }



        public string StreetName { get; set; }
        public int PostalCode { get; set; }
        public int IdPotentiel { get; set; }

        public PotentielPharmacy PotentielPharmacy { get; set; }

        public string FirstNameOwner { get; set; }
        public string LastNameOwner { get; set; }
        public string Email { get; set; }

    }
    public partial class PotentielPharmacy
    {
        public int IdPotentiel { get; set; }
        public string NamePotentiel { get; set; }
    }
}
