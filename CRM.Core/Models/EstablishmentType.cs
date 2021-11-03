using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class EstablishmentType
    {
        public EstablishmentType()
        {
            Establishment = new HashSet<Establishment>();
        }

        public int IdEstablishmentType { get; set; }

        public int Version { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<Establishment> Establishment { get; set; }
    }
}
