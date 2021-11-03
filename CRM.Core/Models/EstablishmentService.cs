using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class EstablishmentService 
    {
        public int IdEstablishment { get; set; }
        public Status StatusEstablishment { get; set; }

        public int VersionEstablishment { get; set; }
        public int Version { get; set; }

        public int IdService { get; set; }
        public Status StatusService { get; set; }

        public int VersionService { get; set; }

        public virtual Establishment IdEstablishmentNavigation { get; set; }
        public virtual Service IdServiceNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
    }
}
