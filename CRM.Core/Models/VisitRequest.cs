﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class VisitRequest
    {
        public int IdVisitRequest { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Comment { get; set; }
        public string TypeVisit { get; set; }
        public string Request { get; set; }
        public string Urgency { get; set; }

        public DateTime DateRequest { get; set; }
        public DateTime Deadline { get; set; }
        public int IdUser { get; set; }
        public Status StatusUser { get; set; }

        public int VersionUser { get; set; }
        public virtual User IdUserNavigation { get; set; }

        public ICollection<VisitRequestReport> VisitRequestReport { get; set; }



    }
}
