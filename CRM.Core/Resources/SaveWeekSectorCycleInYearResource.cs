using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveWeekSectorCycleInYearResource 
    {
        public int IdCycle { get; set; }
   
        public int IdWeekCycle { get; set; }

        public int IdSector { get; set; }

        public int Order { get; set; }

        public int Year { get; set; }

        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public Status Status { get; set; }

        public int Active { get; set; }
    }
}
