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
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public int? IdDoctor { get; set; }

        public int IdService { get; set; }
     
        public int Order { get; set; }
        public int Primary { get; set; }

        public bool ChefService { get; set; }

    }
}
