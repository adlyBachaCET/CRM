using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class WeekInYearResource 
    {
  
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Year { get; set; }
        public int Order { get; set; }
        public int? Lock { get; set; }
        public Status Status { get; set; }

    }
}
