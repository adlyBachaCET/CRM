using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class CycleResource
    {
  
        public Status Status { get; set; }
        public int Active { get; set; }
        public int Version { get; set; }

        public int IdCycle { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Description { get; set; }
        public int? NbDays { get; set; }
        public int? NbSemaine { get; set; }


    }
}
