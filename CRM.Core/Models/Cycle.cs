using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Cycle 
    {
        public Cycle()
        {
            CycleBu = new HashSet<CycleBu>();
            CycleSectorWeekDoctors = new HashSet<CycleSectorWeekDoctors>();
            PotentielCycle = new HashSet<PotentielCycle>();
            WeekSectorCycle = new HashSet<WeekSectorCycle>();
            WeekSectorCycleInYear = new HashSet<WeekSectorCycleInYear>();
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
        public virtual ICollection<CycleSectorWeekDoctors> CycleSectorWeekDoctors { get; set; }
        public virtual ICollection<PotentielCycle> PotentielCycle { get; set; }
        public virtual ICollection<WeekSectorCycle> WeekSectorCycle { get; set; }
        public virtual ICollection<WeekSectorCycleInYear> WeekSectorCycleInYear { get; set; }
    }
}
