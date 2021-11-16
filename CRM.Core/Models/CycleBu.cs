using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class CycleBu 
    {
        public int IdCycle { get; set; }
        public Status StatusCycle { get; set; }

        public int IdBu { get; set; }
        public Status StatusBu { get; set; }
        public int Version { get; set; }
        public int VersionBu { get; set; }
        public int VersionCycle { get; set; }
        public string NameBu { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public virtual BusinessUnit IdBuNavigation { get; set; }
        public virtual Cycle IdCycleNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
    }
}
