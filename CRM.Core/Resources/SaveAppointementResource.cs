using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SaveAppointementResource
    {




        public DateTime Start { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int IdUser { get; set; }
        public int? IdDoctor { get; set; }
        public int? IdPharmacy { get; set; }




    }
}
