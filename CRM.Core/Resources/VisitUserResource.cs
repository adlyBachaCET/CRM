using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class VisitUserResource
    {
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdVisit { get; set; }
        public int VersionVisit { get; set; }
        public Status StatusVisit { get; set; }

        public int IdUser { get; set; }
        public int VersionUser { get; set; }
        public Status StatusUser { get; set; }

    }
}
