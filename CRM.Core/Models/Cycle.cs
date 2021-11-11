using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Cycle 
    {
        public Cycle()
        {
            CycleBu = new HashSet<CycleBu>();
            CycleSectorWeekDoctors = new HashSet<Target>();
            PotentielCycle = new HashSet<PotentielCycle>();
            WeekSectorCycle = new HashSet<SectorCycle>();
            WeekSectorCycleInYear = new HashSet<SectorCycleInYear>();
        }
        public Status Status { get; set; }
        public int Active { get; set; }
        public int Version { get; set; }

        public int IdCycle { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Description { get; set; }
        public int? NbDays { get; set; }
        public int? NbSemaine { get; set; }

        public virtual ICollection<CycleBu> CycleBu { get; set; }
        public virtual ICollection<Target> CycleSectorWeekDoctors { get; set; }
        public virtual ICollection<PotentielCycle> PotentielCycle { get; set; }
        public virtual ICollection<SectorCycle> WeekSectorCycle { get; set; }
        public virtual ICollection<SectorCycleInYear> WeekSectorCycleInYear { get; set; }
    }
}
