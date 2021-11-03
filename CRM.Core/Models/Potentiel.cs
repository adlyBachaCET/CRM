using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Potentiel
    {
        public Potentiel()
        {
            PotentielCycle = new HashSet<PotentielCycle>();
        }
        public Status Status { get; set; }
        public int Version { get; set; }

        public int IdPotentiel { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public string Name { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<PotentielCycle> PotentielCycle { get; set; }
    }
}
