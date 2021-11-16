using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class ProductVisitReport
    {
        public int IdReport{ get; set; }
        public Status StatusReport{ get; set; }

        public int IdProduct { get; set; }
        public Status StatusProduct { get; set; }
        public int VersionProduct { get; set; }
        public int NameProduct { get; set; }

        public int VersionReport{ get; set; }
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
        public virtual Product Product { get; set; }
        public virtual VisitReport Report{ get; set; }
    }
}
