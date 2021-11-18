using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveCycleResource
    {
  
        public string Name { get; set; }

        public string Description { get; set; }
        public int? NbDays { get; set; }
        public int? NbSemaine { get; set; }

    }
}
