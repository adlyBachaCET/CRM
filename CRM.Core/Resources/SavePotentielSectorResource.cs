using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SavePotentielSectorResource
    {
        public int IdSector { get; set; }
       

        public int IdPotentiel { get; set; }
     

        public int Active { get; set; }
        public float Total { get; set; }

    }
}
