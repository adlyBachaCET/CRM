using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SectorLocality 
    {
        public int IdLocality { get; set; }
        public Status StatusLocality { get; set; }

        public int VersionLocality { get; set; }
        public int IdSector { get; set; }
        public Status StatusSector { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int VersionSector { get; set; }
        public int Version { get; set; }

        public virtual Locality IdLocalityNavigation { get; set; }
        public virtual Sector IdSectorNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
    }
}
