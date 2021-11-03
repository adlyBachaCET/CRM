using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class WeekInCycle 
    {
        public WeekInCycle()
        {
            CycleSectorWeekDoctors = new HashSet<CycleSectorWeekDoctors>();
            WeekSectorCycle = new HashSet<WeekSectorCycle>();
            WeekSectorCycleInYear = new HashSet<WeekSectorCycleInYear>();
        }
        public int Version { get; set; }

        public string Name { get; set; }
        public int Active { get; set; }
        public int IdWeekCycle { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<CycleSectorWeekDoctors> CycleSectorWeekDoctors { get; set; }
        public virtual ICollection<WeekSectorCycle> WeekSectorCycle { get; set; }
        public virtual ICollection<WeekSectorCycleInYear> WeekSectorCycleInYear { get; set; }
    }
}
