using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveSpecialityDoctorResource 
    {
        public int IdDoctor { get; set; }

        public int IdSpecialty { get; set; }
 

        public int Version { get; set; }


        public Status Status { get; set; }

        public int Active { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
