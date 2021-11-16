using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class AdresseResource
    {
        public int IdAdresse { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }
        public virtual ICollection<AdresseLocality> AdresseLocality { get; set; }
        public virtual Location Establishment { get; set; }

        public virtual User User{ get; set; }


    }
}
