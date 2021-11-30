using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class ProductSample
    {
        public int IdProductSample { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string Designation { get; set; }
        public int Quantity{ get; set; }
        public int QuantityApprovisionning { get; set; }
        public string Reference { get; set; }

        public string Description { get; set; }

        public ICollection<ProductVisitReport> ProductSampleVisitReport { get; set; }
    }
}
