using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class VisitChannel
    {
        public int IdChannel { get; set; }

        public int Version { get; set; }
        public Status Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
   
        public int IdVisit { get; set; }
        public int VersionVisit { get; set; }
        public Status StatusVisit { get; set; }
        public virtual Visit Visit { get; set; }
        public string context { get; set; }



    }
}
