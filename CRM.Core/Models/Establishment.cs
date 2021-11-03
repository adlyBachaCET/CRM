using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Establishment 
    {
        public Establishment()
        {
            EstablishmentDoctor = new HashSet<EstablishmentDoctor>();
            EstablishmentLocality = new HashSet<EstablishmentLocality>();
            EstablishmentService = new HashSet<EstablishmentService>();
            EstablishmentUser = new HashSet<EstablishmentUser>();
        }
        public int IdEstablishment { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public int? IdEstablishmentType { get; set; }
        public int VersionEstablishmentType { get; set; }
        public Status StatusEstablishmentType { get; set; }
        public string Name { get; set; }
        public string Adresse { get; set; }
        public int? Primary { get; set; }

        public virtual EstablishmentType IdEstablishmentTypeNavigation { get; set; }
        public virtual ICollection<EstablishmentDoctor> EstablishmentDoctor { get; set; }
        public virtual ICollection<EstablishmentLocality> EstablishmentLocality { get; set; }
        public virtual ICollection<EstablishmentService> EstablishmentService { get; set; }
        public virtual ICollection<EstablishmentUser> EstablishmentUser { get; set; }
    }
}
