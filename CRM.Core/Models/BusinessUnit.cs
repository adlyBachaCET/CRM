using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class BusinessUnit 
    {
        public BusinessUnit()
        {
            BuDoctor = new HashSet<BuDoctor>();
            BuUser = new HashSet<BuUser>();
            CycleBu = new HashSet<CycleBu>();
        }
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int IdBu { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BuDoctor> BuDoctor { get; set; }
        public virtual ICollection<BuUser> BuUser { get; set; }
        public virtual ICollection<BuFile> BuFile { get; set; }

        public virtual ICollection<CycleBu> CycleBu { get; set; }
        public virtual ICollection<Product> Product { get; set; }

        public Status Status { get; set; }
    }
}
