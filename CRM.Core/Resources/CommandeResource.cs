using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class CommandeResource
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
        public int IdDoctor { get; set; }
        public string NameDoctor { get; set; }

        public int IdUser{ get; set; }
        public string NomPrenom{ get; set; }


    }
}
