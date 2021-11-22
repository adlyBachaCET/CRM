using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveParticipantResource
    {
  
        public int IdRequestRp{ get; set; }
        public int? IdDoctor { get; set; }
        public int? IdPharmacy { get; set; }

        public int IdUser { get; set; }

        public string Organisme { get; set; }

    }
}
