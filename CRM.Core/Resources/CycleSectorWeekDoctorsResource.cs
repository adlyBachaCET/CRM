using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class CycleSectorWeekDoctorsResource 
    {
        public int IdCycle { get; set; }
        public Status StatusCycle { get; set; }
        public int VersionCycle { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdUser { get; set; }
        public Status StatusUser { get; set; }
        public int VersionUser { get; set; }

        public int IdDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public int VersionDoctor { get; set; }

        public int? IdSector { get; set; }

        public Status StatusSector { get; set; }
        public int VersionSector { get; set; }

        public int? IdWeekCycle { get; set; }
        public Status StatusWeekCycle { get; set; }
        public int VersionWeekCycle { get; set; }

        public Status Status { get; set; }
        public int Version { get; set; }

  
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
    }
}
