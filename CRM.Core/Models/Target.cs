﻿using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Target 
    {
        public string IdTarget { get; set; }
        public int IdCycle { get; set; }
        public Status StatusCycle { get; set; }
        public int VersionCycle { get; set; }


        public int IdUser { get; set; }
        public Status StatusUser { get; set; }
        public int VersionUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IdDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public int VersionDoctor { get; set; }

        public int? IdSector { get; set; }

        public Status StatusSector { get; set; }
        public int VersionSector { get; set; }

  

        public Status Status { get; set; }
        public int Version { get; set; }

        public virtual Cycle IdCycleNavigation { get; set; }
        public virtual Doctor IdDoctorNavigation { get; set; }
        public virtual Sector IdSectorNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public virtual ICollection<PlanificationTarget> PlanificationTarget { get; set; }

    }
}