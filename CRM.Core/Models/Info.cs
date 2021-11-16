using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Info
    {
        public int IdInf { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Version { get; set; }

        public int Active { get; set; }
        public string Datatype { get; set; }
        public string TypeInfo { get; set; }
    
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string Data { get; set; }
        public Status Status { get; set; }
        public int? IdDoctor { get; set; }
        public int VersionDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public virtual Doctor IdDoctorNavigation { get; set; }
    }
}
