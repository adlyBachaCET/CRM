using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Brick
    {
        public Brick()
        {
            BrickLocality = new HashSet<BrickLocality>();
        }

        public int IdBrick { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<BrickLocality> BrickLocality { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
        public int Version { get; set; }
    }
}
