using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SaveCommandeResource
    {

 

       
        public string Designation { get; set; }
        public DateTime? DateLivraison { get; set; }

        public int TotalHT {get; set; }
        public int TotalTTC { get; set; }
        public int? IdDoctor { get; set; }

        public int IdUser{ get; set; }
        public int? IdPharmacy { get; set; }


    }
}
