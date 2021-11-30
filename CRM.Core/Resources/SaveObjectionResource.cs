
using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveObjectionResource
    {
 


        public string Description { get; set; }
        public int IdDoctor { get; set; }
        public int IdUser { get; set; }

        public int IdPharmacy { get; set; }
        public int Satisfaction { get; set; }
        public int? IdProduct { get; set; }

        public string Commentary { get; set; }
        public bool OnProduct { get; set; }

    }

}
