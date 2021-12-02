using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class LocationDoctorResource
    {

        public LocationResource Location { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public ServiceResource Service { get; set; }


        public int Order { get; set; }
        public int Primary { get; set; }


    }
}
