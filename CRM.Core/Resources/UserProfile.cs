
using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class UserProfile
    {

        public SaveUserResourceWithoutPassword User {get;set;}

        public SaveBusinessUnitResource BusinessUnit { get; set; }
        public IEnumerable<UserResource> UserOfBu { get; set; }
        public IEnumerable<RequestDoctorResource> RequestDoctor { get; set; }

        public IEnumerable<ObjectionResource> Objection { get; set; }
        public IEnumerable<VisitResource> Visit { get; set; }
        public IEnumerable<RequestRpResource> RequestRp { get; set; }
        public IEnumerable<CommandeResource> Commande { get; set; }



    }

}
