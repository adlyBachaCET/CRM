using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class WholeSalerLocality 
    {
        public int IdLocality { get; set; }
        public Status StatusLocality { get; set; }

        public int VersionLocality { get; set; }
        public int IdPwholeSaler { get; set; }
        public Status StatusPwholeSaler { get; set; }

        public int VersionPwholeSaler { get; set; }
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public virtual Locality IdLocalityNavigation { get; set; }
        public virtual WholeSaler IdPwholeSalerNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
    }
}
