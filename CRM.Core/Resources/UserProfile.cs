
using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class UserProfile
    {

        public SaveUserResourceWithoutPassword User {get;set;}

        public SaveBusinessUnitResource BusinessUnit { get; set; }
        public IEnumerable<User> UserOfBu { get; set; }
        public IEnumerable<RequestDoctor> RequestDoctor { get; set; }

        public IEnumerable<Objection> Objection { get; set; }
        public IEnumerable<Visit> Visit { get; set; }
        public IEnumerable<RequestRp> RequestRp { get; set; }
        public IEnumerable<Commande> Commande { get; set; }



    }

}
