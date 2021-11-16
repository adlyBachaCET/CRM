using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class BuDoctor
    {
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdBu { get; set; }
        public Status StatusBu { get; set; }
        public int VersionBu { get; set; }

        public string NameBu { get; set; }

        public int IdDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public int VersionDoctor { get; set; }

        public int Version { get; set; }


        public virtual BusinessUnit IdBuNavigation { get; set; }
        public virtual Doctor IdDoctorNavigation { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }

    }
}
