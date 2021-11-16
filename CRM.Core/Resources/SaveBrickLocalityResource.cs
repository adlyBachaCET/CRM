using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveBrickLocalityResource 
    {
        public int IdLocality { get; set; }

        public int IdBrick { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


    }
}
