using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class BrickLocalityResource
    {
        public int IdLocality { get; set; }
        public Status StatusLocality { get; set; }

        public int IdBrick { get; set; }
        public Status StatusBrick { get; set; }
        public int VersionBrick { get; set; }
        public int VersionLocality { get; set; }
        public int Version { get; set; }


        public virtual Brick IdBrickNavigation { get; set; }
        public virtual Locality IdLocalityNavigation { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }

    }
}
