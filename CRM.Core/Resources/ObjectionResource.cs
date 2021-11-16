using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class ObjectionResource
    {
        public int IdObjection { get; set; }
        public int Active { get; set; }
        public int Version { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Description { get; set; }



        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? IdDoctor { get; set; }
        public int VersionDoctor { get; set; }
        public Status StatusDoctor { get; set; }

   
        public Status Status { get; set; }
        public virtual Doctor Doctor { get; set; }
     

    }
  
}
