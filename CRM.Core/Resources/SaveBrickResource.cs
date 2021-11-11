using CRM.Core.Models;
using System;
using System.Collections.Generic;

namespace CRM_API.Resources
{
    public class SaveBrickResource
    {
        public SaveBrickResource()
        {
            BrickLocality = new HashSet<BrickLocality>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<BrickLocality> BrickLocality { get; set; }

        public int? Active { get; set; }
    }
}
