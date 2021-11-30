
using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class DoctorListObject
    {
        public DoctorResource Doctor { get; set; }

        public List<BusinessUnitResource> BusinessUnit { get; set; }
        public List<TagsResource> Tags { get; set; }
        public List<PhoneResource> Phone { get; set; }

        public List<LocationDoctorResource> LocationDoctor { get; set; }



    }

}
