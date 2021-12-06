using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class CommandeProduct
    {

        public int IdCommandeProduct { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int Active { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
       
        public string Designation { get; set; }
        public DateTime DateLivraison { get; set; }

        public int TotalHT {get; set; }
        public int TotalTTC { get; set; }
        public int IdCommande { get; set; }
        public int VersionCommande { get; set; }
        public Status StatusCommande { get; set; }
        public Commande Commande { get; set; }
        public int IdProduct { get; set; }
        public int VersionProduct { get; set; }
        public Status StatusProduct { get; set; }
        public Product Product { get; set; }

    }
}
