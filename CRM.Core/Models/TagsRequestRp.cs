using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class TagsRequestRp
    {
        public int IdRequestRp{ get; set; }
        public Status StatusRequestRp{ get; set; }

        public int VersionRequestRp{ get; set; }
        public int IdTags { get; set; }
        public Status StatusTags { get; set; }

        public int VersionTags { get; set; }

        public int Version { get; set; }

        public virtual RequestRp IdRequestRpNavigation { get; set; }
        public virtual Tags IdTagsNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
    }
}
