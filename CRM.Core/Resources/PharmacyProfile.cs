
using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class PharmacyProfile
    {

        public PharmacyResource Pharmacy { get;set;}

        public List<PhoneResource> Phone { get; set; }

        public List<VisitReportResource> VisitReports { get; set; }
        public List<ObjectionResource> Objection { get; set; }
        public List<ObjectionResource> Request { get; set; }

        public List<RequestRpResource> RequestRp { get; set; }
        public List<CommandeResource> Commande { get; set; }
        public List<ProductResource> Product { get; set; }

    }
    public partial class PharmacyObjectList
    {

        public PharmacyResource Pharmacy { get; set; }

        public List<PhoneResource> Phone { get; set; }



    }
}
