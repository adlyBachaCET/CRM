using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class ProductSampleVisitReport
    {
        public int IdReport{ get; set; }
        public Status StatusReport{ get; set; }

        public int IdProductSample { get; set; }
        public Status StatusProductSample { get; set; }
        public int VersionProductSample { get; set; }
        public int NameProductSample { get; set; }

        public int VersionReport{ get; set; }
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
        public virtual ProductSample ProductSample { get; set; }
        public virtual VisitReport Report{ get; set; }
        public int Quatity { get; set; }

    }
}
