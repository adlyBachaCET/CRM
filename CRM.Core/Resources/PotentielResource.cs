using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class PotentielResource
    {
 
        public Status Status { get; set; }
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdPotentiel { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public string Name { get; set; }


    }
}
