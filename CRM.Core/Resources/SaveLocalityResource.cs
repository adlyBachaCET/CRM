using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveLocalityResource
    {
  

        public int Version { get; set; }

        public string Name { get; set; }

        public int Active { get; set; }

        public Status Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int lvl { get; set; }

        public int? IdParent { get; set; }


    }
}
