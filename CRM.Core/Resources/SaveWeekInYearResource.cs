using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveWeekInYearResource
    {
   

        public string Name { get; set; }

        public int Year { get; set; }
        public int Order { get; set; }
        public int? Lock { get; set; }


    }
}
