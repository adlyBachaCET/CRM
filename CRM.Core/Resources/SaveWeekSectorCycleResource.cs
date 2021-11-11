using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveWeekSectorCycleResource 
    {
        public int IdCycle { get; set; }


        public int Version { get; set; }
        public int IdWeekCycle { get; set; }


        public int IdSector { get; set; }

        public int VersionSector { get; set; }



        public int Active { get; set; }
        public int nbvisite { get; set; }
        public Status Status { get; set; }
    }
}
