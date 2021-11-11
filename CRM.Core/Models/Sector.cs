﻿using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Sector 
    {
        public Sector()
        {
            CycleSectorWeekDoctors = new HashSet<Target>();
            SectorLocality = new HashSet<SectorLocality>();
            WeekSectorCycle = new HashSet<SectorCycle>();
            WeekSectorCycleInYear = new HashSet<SectorCycleInYear>();
        }
        public int Version { get; set; }

        public int Active { get; set; }
        public int IdSector { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<Target> CycleSectorWeekDoctors { get; set; }
        public virtual ICollection<SectorLocality> SectorLocality { get; set; }
        public virtual ICollection<SectorCycle> WeekSectorCycle { get; set; }
        public virtual ICollection<SectorCycleInYear> WeekSectorCycleInYear { get; set; }
    }
}
