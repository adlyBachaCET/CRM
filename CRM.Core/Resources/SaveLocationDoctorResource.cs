using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveLocationDoctorResource
    {



        public int IdLocation{ get; set; }

        public int IdDoctor { get; set; }

        public int IdService { get; set; }
     
        public int Order { get; set; }
        public int Primary { get; set; }

        public bool ChiefService { get; set; }

    }
}
