
using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class AdresseLocality
    {
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public int IdAdresse { get; set; }
        public int VersionAdresse { get; set; }
        public Status StatusAdresse { get; set; }

        public int IdLocality { get; set; }
        public int VersionLocality { get; set; }
        public Status StatusLocality { get; set; }

        public virtual Adresse IdAdresseNavigation { get; set; }
        public virtual Locality IdLocalityNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int Version { get; set; }

    }
}
