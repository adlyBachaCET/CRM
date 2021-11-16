using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class LocationType
    {
      

        public int IdLocationType { get; set; }

        public int Version { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<Location> Location { get; set; }
    }
}
