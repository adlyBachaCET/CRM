using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Tags
    {
        public Tags()
        {
            TagsDoctor = new HashSet<TagsDoctor>();
        }
        public int Version { get; set; }

        public int IdTags { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<TagsDoctor> TagsDoctor { get; set; }
        public virtual ICollection<TagsRequestRp> TagsRequestRp { get; set; }

    }
}
