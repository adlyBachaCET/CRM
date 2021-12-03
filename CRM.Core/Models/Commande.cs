using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class Commande
    {
        public int IdCommande { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int Active { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
       
        public string Designation { get; set; }
        public DateTime? DateLivraison { get; set; }

        public int TotalHT {get; set; }
        public int TotalTTC { get; set; }
        public int? IdDoctor { get; set; }
        public int? VersionDoctor { get; set; }
        public Status? StatusDoctor { get; set; }
        public virtual Doctor Doctor { get; set; }
        public string Name { get; set; }

        public string NomPrenom{ get; set; }
        public int IdUser { get; set; }
        public int VersionUser { get; set; }
        public Status StatusUser { get; set; }
        public virtual User User { get; set; }

        public int? IdPharmacy { get; set; }
        public int? VersionPharmacy { get; set; }
        public Status? StatusPharmacy { get; set; }
        public virtual Pharmacy Pharmacy { get; set; }
        public virtual ICollection<CommandeProduct> CommandeProducts { get; set; }

    }
}
