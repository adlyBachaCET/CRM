using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{


    public partial class PharmacyExiste
    {
        public Pharmacy PharmacyName { get; set; }
        public bool ExistPharmacyName { get; set; }
        public Pharmacy PharmacyFirstName { get; set; }
        public bool ExistPharmacyFirstName { get; set; }
        public Pharmacy PharmacyLastName { get; set; }
        public bool ExistPharmacyLastName { get; set; }
        public Pharmacy PharmacyEmail { get; set; }
        public bool ExistPharmacyEmail{ get; set; }
    }
}
