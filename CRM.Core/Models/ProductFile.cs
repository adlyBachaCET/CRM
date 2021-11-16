using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class ProductFile 
    {
        public int IdProduct { get; set; }
        public Status StatusProduct { get; set; }

        public int IdFile { get; set; }
        public Status StatusFile { get; set; }
        public int Version { get; set; }
        public int VersionFile { get; set; }
        public int VersionProduct { get; set; }
        public int NameFile { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public virtual File IdFileNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
    }
}
