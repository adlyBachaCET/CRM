using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SectorCycleResource 
    {
        public int IdCycle { get; set; }
        public Status StatusCycle { get; set; }

        public int VersionCycle { get; set; }

        public int Version { get; set; }


        public int IdSector { get; set; }
        public Status StatusSector { get; set; }

        public int VersionSector { get; set; }

        public virtual Cycle IdCycleNavigation { get; set; }
        public virtual Sector IdSectorNavigation { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
    }
}
