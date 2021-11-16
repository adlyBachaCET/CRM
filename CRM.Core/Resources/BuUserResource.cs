using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class BuUserResource 
    {
        public int IdUser { get; set; }
        public Status StatusUser { get; set; }

        public int IdBu { get; set; }
        public Status StatusBu { get; set; }
        public int VersionBu { get; set; }
        public int VersionUser { get; set; }
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
        public virtual BusinessUnit IdBuNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
