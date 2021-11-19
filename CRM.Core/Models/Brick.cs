using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Brick
    {

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdBrick { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int PostalCode { get; set; }
        public int NumSystemBrick { get; set; }

        public Status Status { get; set; }
        public int Version { get; set; }
    }
}
