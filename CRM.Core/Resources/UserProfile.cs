
using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class UserProfile
    {

        public SaveUserResourceWithoutPassword User {get;set;}

        public SaveBusinessUnitResource BusinessUnit { get; set; }
        public IEnumerable<User> UserOfBu { get; set; }
    }

}
