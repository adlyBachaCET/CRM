using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class PlanificationTarget
    {
        public int IdTarget{ get; set; }
        public Status StatusTarget{ get; set; }

        public int IdPlanification { get; set; }
        public Status StatusPlanification { get; set; }
        public int Version { get; set; }
        public int VersionPlanification { get; set; }
        public int VersionTarget{ get; set; }
        public int NamePlanification { get; set; }

        public virtual Planification Planification { get; set; }
        public virtual Target Target { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
    }
}
