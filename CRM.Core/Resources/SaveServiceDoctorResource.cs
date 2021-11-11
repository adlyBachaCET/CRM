using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveServiceDoctorResource 
    {
        public int IdService { get; set; }

        public int IdDoctor { get; set; }

        public int Version { get; set; }

        public Status Status { get; set; }

        public int Active { get; set; }
    }
}
