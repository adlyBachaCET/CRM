
using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class UserProfile
    {

        public SaveUserResourceWithoutPassword User {get;set;}

        public SaveBusinessUnitResource BusinessUnit { get; set; }
        public List<UserResource> UserOfBu { get; set; }

        public List<ObjectionResource> Objection { get; set; }
        public List<VisitResource> Visit { get; set; }
        public List<RequestRpResource> RequestRp { get; set; }
        public List<CommandeResource> Commande { get; set; }



    }

}
