using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class EstablishmentDoctor 
    {
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int IdEstablishment { get; set; }
        public Status StatusEstablishment { get; set; }
        public int VersionEstablishment { get; set; }

        public int IdDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public int VersionDoctor { get; set; }
        public int Version { get; set; }

        public Status Status { get; set; }
        public virtual Doctor IdDoctorNavigation { get; set; }
        public virtual Establishment IdEstablishmentNavigation { get; set; }
    }
}
