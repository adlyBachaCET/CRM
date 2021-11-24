using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class PotentielSectorResource
    {
        public int IdSector { get; set; }
        public int VersionSector { get; set; }
        public Status StatusSector { get; set; }

        public int IdPotentiel { get; set; }
        public Status StatusPotentiel { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int VersionPotentiel { get; set; }
        public int Version { get; set; }

        public Status Status { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public float Total { get; set; }

    }
}
