using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class WeekSectorCycleResource 
    {
        public int IdCycle { get; set; }
        public Status StatusCycle { get; set; }

        public int VersionCycle { get; set; }

        public int Version { get; set; }
        public int IdWeekCycle { get; set; }
        public Status StatusWeekCycle { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int VersionWeekCycle { get; set; }

        public int IdSector { get; set; }
        public Status StatusSector { get; set; }

        public int VersionSector { get; set; }

        public virtual Cycle IdCycleNavigation { get; set; }
        public virtual Sector IdSectorNavigation { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int nbvisite { get; set; }
        public Status Status { get; set; }
    }
}
