﻿using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveSectorCycleResource 
    {
        public int IdCycle { get; set; }
        public int IdSector { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
