using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class Product
    {
        public int IdProduct { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int Active { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
       
        public string Designation { get; set; }
        public int Quantity{ get; set; }
        public int QuantityApprovisionning { get; set; }
        public string Reference { get; set; }

        public int PriceHT {get; set; }
        public int PriceTTC { get; set; }
        public int TVA { get; set; }
        public string Description { get; set; }
        public bool HasSample { get; set; }

        public int? IdBu { get; set; }
        public int? VersionBu { get; set; }
        public Status? StatusBu { get; set; }
        public BusinessUnit Bu { get; set; }
        public virtual ICollection<ProductFile> ProductFile { get; set; }
        public virtual ICollection<ProductPharmacy> ProductPharmacy { get; set; }
        public virtual ICollection<Objection> Objection { get; set; }

        public ICollection<ProductVisitReport> ProductVisitReport { get; set; }
        public virtual ICollection<CommandeProduct> CommandeProducts { get; set; }

    }
}
