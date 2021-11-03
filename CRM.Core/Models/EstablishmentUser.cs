using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class EstablishmentUser 
    {
        public DateTime? UpdatedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int Active { get; set; }
        public int IdEstablishment { get; set; }
        public Status StatusEstablishment { get; set; }

        public int VersionEstablishment { get; set; }
        public int Version { get; set; }
        public int IdUser { get; set; }
        public Status StatusUser { get; set; }

        public int VersionUser { get; set; }
        public Status Status { get; set; }
        public virtual Establishment IdEstablishmentNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
