using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class Activity
    {
        public int IdActivity { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public virtual ICollection<ActivityUser> ActivityUser { get; set; }


    }
}
