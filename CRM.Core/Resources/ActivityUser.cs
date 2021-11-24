using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class ActivityUser 
    {
        public int IdUser { get; set; }
        public Status StatusUser { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public int IdActivity { get; set; }
        public Status StatusActivity { get; set; }
        public int VersionActivity { get; set; }
        public int NameActivity { get; set; }

        public int VersionUser { get; set; }
        public int Version { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
        public virtual Activity Activity { get; set; }
        public virtual User User { get; set; }
    }
}
