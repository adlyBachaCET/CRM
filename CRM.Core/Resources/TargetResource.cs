using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class TargetResource
    {
        public int IdCycle { get; set; }
        public int IdUser { get; set; }
        public int? IdDoctor { get; set; }
        public int? IdPharmacy { get; set; }
        public int IdSector { get; set; }
        public string Note { get; set; }
        public int NumTarget { get; set; }

    }
}
