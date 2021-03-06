using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Service 
    {
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int IdService { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public int? tel { get; set; }
        public virtual ICollection<LocationDoctor> LocationDoctor { get; set; }

    }
}
