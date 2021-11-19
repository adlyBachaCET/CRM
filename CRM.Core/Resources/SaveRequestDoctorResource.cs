using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SaveRequestDoctorResource
    {
 

        public int? IdDoctor { get; set; }
        public int IdUser { get; set; }
        public int? IdPharmacy { get; set; }

        public string Description { get; set; }



    }
}
