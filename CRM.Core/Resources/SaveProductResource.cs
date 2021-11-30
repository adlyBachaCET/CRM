using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SaveProductResource
    {
        
       
        public string Designation { get; set; }
        public int Quantity{ get; set; }
        public int QuantityApprovisionning { get; set; }
        public string Reference { get; set; }
        public int PriceHT {get; set; }
        public int PriceTTC { get; set; }
        public int TVA { get; set; }
        public string Description { get; set; }
        public int IdBu { get; set; }
        public bool HasSample { get; set; }


    }
}
