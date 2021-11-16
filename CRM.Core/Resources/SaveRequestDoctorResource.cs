using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SaveRequestDoctorResource
    {
 
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public int IdDoctor { get; set; }
     
        public string Name { get; set; }
        public string Description { get; set; }



    }
}
