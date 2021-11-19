
using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class DoctorProfile
    {

        public DoctorResource Doctor { get;set;}

        public List<BusinessUnitResource> BusinessUnit { get; set; }
        public List<TagsResource> Tags { get; set; }
        public List<InfoResource> Infos { get; set; }
        public List<PhoneResource> Phone { get; set; }

        public List<LocationDoctorResource> LocationDoctor { get; set; }
        public List<VisitReportResource> VisitReports { get; set; }
        public List<ObjectionResource> Objection { get; set; }
        public List<RequestDoctorResource> RequestDoctors { get; set; }
        public List<RequestRpResource> RequestRp { get; set; }
        public List<CommandeResource> Commande { get; set; }

    }

}
