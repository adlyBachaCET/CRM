﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class RequestDoctorResource
    {
        public int IdRequestDoctor { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
     
        public string Name { get; set; }
        public string Description { get; set; }
        public int? IdDoctor { get; set; }
        public int? VersionDoctor { get; set; }
        public Status? StatusDoctor { get; set; }

        public int? IdPharmacy { get; set; }
        public int? VersionPharmacy { get; set; }
        public Status? StatusPharmacy { get; set; }
        public StatusRequest StatusRequest { get; set; }
        public string Response { get; set; }
        public StatusSatisfaction StatusSatisfaction { get; set; }

        public StatusCompletion StatusCompletion { get; set; }

    }
}
