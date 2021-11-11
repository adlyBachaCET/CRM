using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class LocationDoctor 
    {
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int IdLocation{ get; set; }
        public Status StatusLocation{ get; set; }
        public int VersionLocation{ get; set; }

        public int IdDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public int VersionDoctor { get; set; }
        public int Version { get; set; }

        public Status Status { get; set; }
        public virtual Doctor IdDoctorNavigation { get; set; }
        public virtual Location IdLocationNavigation { get; set; }
        public int IdService { get; set; }
        public Status StatusService { get; set; }

        public int VersionService { get; set; }
        public virtual Service IdServiceNavigation { get; set; }
        public int Order { get; set; }
        public bool ChefService { get; set; }

    }
}
