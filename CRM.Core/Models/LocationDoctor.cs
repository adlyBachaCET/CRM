using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class LocationDoctor
    {
        public int IdLocationDoctorService { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
        public int Version { get; set; }

        public int IdLocation{ get; set; }
        public Status StatusLocation { get; set; }
        public int VersionLocation { get; set; }
        public Location Location { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public int? IdDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public int VersionDoctor { get; set; }
        public Doctor Doctor { get; set; }

        public int IdService { get; set; }
        public Status StatusService { get; set; }
        public int VersionService { get; set; }
        public Service Service { get; set; }
        public int Order { get; set; }
        public int Primary { get; set; }


    }
}
