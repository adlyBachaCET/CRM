using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveProductPharmacyResource
    {
        public int IdProduct { get; set; }
        public int? IdPharmacy{ get; set; }
        public int Quantity { get; set; }



    }
}
