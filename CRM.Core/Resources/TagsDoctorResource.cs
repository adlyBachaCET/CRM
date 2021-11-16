using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class TagsDoctorResource
    {
        public int IdDoctor { get; set; }
        public Status StatusDoctor { get; set; }

        public int VersionDoctor { get; set; }
        public int IdTags { get; set; }
        public Status StatusTags { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int VersionTags { get; set; }

        public int Version { get; set; }

        public virtual Doctor IdDoctorNavigation { get; set; }
        public virtual Tags IdTagsNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
    }
}
