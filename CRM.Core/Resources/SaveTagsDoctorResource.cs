using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveTagsDoctorResource
    {
        public int IdDoctor { get; set; }

        public int IdTags { get; set; }


        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
