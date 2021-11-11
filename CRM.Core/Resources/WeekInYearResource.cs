using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class WeekInYearResource 
    {
        public WeekInYearResource()
        {
            WeekSectorCycleInYearOrderNavigation = new HashSet<SectorCycleInYear>();
            WeekSectorCycleInYearYearNavigation = new HashSet<SectorCycleInYear>();
        }
        public int Version { get; set; }

        public string Name { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Year { get; set; }
        public int Order { get; set; }
        public int? Lock { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<SectorCycleInYear> WeekSectorCycleInYearOrderNavigation { get; set; }
        public virtual ICollection<SectorCycleInYear> WeekSectorCycleInYearYearNavigation { get; set; }
    }
}
