using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveWeekInYearResource
    {
   
        public int Version { get; set; }

        public string Name { get; set; }
        public int Active { get; set; }

        public int Year { get; set; }
        public int Order { get; set; }
        public int? Lock { get; set; }
        public Status Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
