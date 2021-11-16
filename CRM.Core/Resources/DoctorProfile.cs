
using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class DoctorProfile
    {

        public Doctor Doctor { get;set;}

        public List<BusinessUnit> BusinessUnit { get; set; }
        public List<Tags> Tags { get; set; }
        public List<Info> Infos { get; set; }
        public List<Phone> Phone { get; set; }

        public List<LocationDoctor> LocationDoctor { get; set; }
    }

}
