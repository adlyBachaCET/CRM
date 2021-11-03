using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class EstablishmentLocality 
    {
        public int IdEstablishment { get; set; }
        public int VersionEstablishment { get; set; }
        public Status StatusEstablishment { get; set; }

        public int IdLocality { get; set; }
        public int VersionLocality { get; set; }
        public Status StatusLocality { get; set; }

        public virtual Establishment IdEstablishmentNavigation { get; set; }
        public virtual Locality IdLocalityNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int Version { get; set; }

    }
}
