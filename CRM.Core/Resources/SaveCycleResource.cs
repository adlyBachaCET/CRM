using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveCycleResource
    {
  
        public Status Status { get; set; }
        public int Active { get; set; }
        public int Version { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public int? NbDays { get; set; }
        public int? NbSemaine { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


    }
}
