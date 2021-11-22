using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class ServiceDoctorResource 
    {
        public int IdService { get; set; }
        public Status StatusService { get; set; }

        public int VersionService { get; set; }
        public int IdDoctor { get; set; }
        public Status StatusDoctor { get; set; }

        public int VersionDoctor { get; set; }
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
    }
}
