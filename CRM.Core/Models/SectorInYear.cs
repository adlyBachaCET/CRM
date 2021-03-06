using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SectorInYear 
    {
  

        public int IdSector { get; set; }
        public Status StatusSector { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int VersionSector { get; set; }
        public int Order { get; set; }
        public int Year { get; set; }

        public Status StatusWeekInYear { get; set; }

        public int VersionWeekInYear { get; set; }

        public int Version { get; set; }

        public virtual Sector IdSectorNavigation { get; set; }
        public virtual WeekInYear OrderNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int Lock { get; set; }
        public bool Request { get; set; }

    }
}
