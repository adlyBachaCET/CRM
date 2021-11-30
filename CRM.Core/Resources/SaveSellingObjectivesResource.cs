using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SaveSellingObjectivesResource
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int? IdPharmacy { get; set; }
        public int? IdDoctor { get; set; }
        public int? IdUser { get; set; }



    }
}
