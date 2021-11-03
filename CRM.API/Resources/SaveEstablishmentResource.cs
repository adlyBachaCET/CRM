using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveEstablishmentResource 
    {

        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
      
        public string Name { get; set; }
        public string Adresse { get; set; }
        public int? Primary { get; set; }

     
    }
}
