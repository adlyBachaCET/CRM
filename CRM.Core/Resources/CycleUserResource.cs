using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class CycleUserResource
    {
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
   
        public int IdCycle { get; set; }
        public int VersionCycle { get; set; }
        public Status StatusCycle { get; set; }
        public virtual Cycle Cycle { get; set; }

        public int IdUser { get; set; }
        public int VersionUser { get; set; }
        public Status StatusUser { get; set; }
        public virtual User User { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
