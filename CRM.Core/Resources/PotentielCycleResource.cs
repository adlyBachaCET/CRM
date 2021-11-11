using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class PotentielCycleResource 
    {
        public int IdCycle { get; set; }
        public int VersionCycle { get; set; }
        public Status StatusCycle { get; set; }

        public int IdPotentiel { get; set; }
        public Status StatusPotentiel { get; set; }

        public int VersionPotentiel { get; set; }
        public int Version { get; set; }

        public Status Status { get; set; }
        public virtual Cycle IdCycleNavigation { get; set; }
        public virtual Potentiel IdPotentielNavigation { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int Freq { get; set; }

    }
}
