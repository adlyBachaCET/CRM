using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class BuFile
    {
        public int IdFile{ get; set; }
        public Status StatusFile{ get; set; }

        public int IdBu { get; set; }
        public Status StatusBu { get; set; }
        public int VersionBu { get; set; }
        public int NameBu { get; set; }

        public int VersionFile{ get; set; }
        public int Version { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
        public virtual BusinessUnit IdBuNavigation { get; set; }
        public virtual File File { get; set; }

    }
}
