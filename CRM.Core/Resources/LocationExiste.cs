using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{


    public partial class LocationExiste
    {
        public Location LocationName { get; set; }
        public bool ExistLocationName { get; set; }
        public Location LocationFirstName { get; set; }
        public bool ExistLocationFirstName { get; set; }
        public Location LocationLastName { get; set; }
        public bool ExistLocationLastName { get; set; }
        public Location LocationEmail { get; set; }
        public bool ExistLocationEmail{ get; set; }
    }
}
