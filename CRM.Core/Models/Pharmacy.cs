using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Pharmacy 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int Active { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
        public virtual ICollection<SellingObjectives> SellingObjectives { get; set; }
        public virtual ICollection<ProductPharmacy> ProductPharmacy { get; set; }
        public virtual ICollection<Commande> Commande { get; set; }

        public string Role { get; set; }//Pharmacy ou grossicte
        public Status Status { get; set; }
        public GrossistePharmacy GrossistePharmacy { get; set; }

        public virtual ICollection<Appointement> Appointement { get; set; }

        public virtual ICollection<Target> Target { get; set; }
        public virtual ICollection<Participant> Participant { get; set; }
        public virtual ICollection<Objection> Objection { get; set; }

        public virtual ICollection<Visit> Visit { get; set; }

        public int IdLocality1 { get; set; }
        public int VersionLocality1 { get; set; }
        public Status StatusLocality1 { get; set; }
        public virtual Locality Locality1 { get; set; }
        public string NameLocality1 { get; set; }


        public int IdLocality2 { get; set; }
        public int VersionLocality2 { get; set; }
        public Status StatusLocality2 { get; set; }
        public virtual Locality Locality2 { get; set; }
        public string NameLocality2 { get; set; }


        public int? IdBrick1 { get; set; }
        public int? VersionBrick1 { get; set; }
        public Status? StatusBrick1 { get; set; }
        public virtual Brick Brick1 { get; set; }
        public int? IdBrick2 { get; set; }
        public int? VersionBrick2 { get; set; }
        public Status? StatusBrick2 { get; set; }
        public virtual Brick Brick2 { get; set; }
        public string NameBrick1 { get; set; }
        public string NameBrick2 { get; set; }
        public int NumBrick1 { get; set; }
        public int NumBrick2 { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }
        public int IdPotentiel { get; set; }
        public string NamePotentiel { get; set; }
        public int VersionPotentiel { get; set; }
        public Status StatusPotentiel { get; set; }
        public virtual Potentiel Potentiel { get; set; }

        public string FirstNameOwner { get; set; }
        public string LastNameOwner { get; set; }
        public string Email { get; set; }
        public int? LinkedId { get; set; }
        public int VersionLink { get; set; }
        public Status StatusLink { get; set; }
        public virtual Pharmacy Linked { get; set; }
        public virtual ICollection<Pharmacy> InverseLinked { get; set; }



    }
}
