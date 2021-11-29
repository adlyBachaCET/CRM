using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveTargetResource
    {
        public int NumTarget { get; set; }

        public int IdCycle { get; set; }
        public int IdUser { get; set; }
        public int? IdDoctor { get; set; }
        public int? IdPharmacy { get; set; }
        public int IdSector { get; set; }
        public string Note { get; set; }
    }
   
  
}
