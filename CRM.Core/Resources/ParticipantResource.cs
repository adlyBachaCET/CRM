using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class ParticipantResource
    {
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int IdRequestRp{ get; set; }
        public Status StatusRequestRp{ get; set; }
        public int VersionRequestRp{ get; set; }

        public int? IdDoctor { get; set; }
        public Status? StatusDoctor { get; set; }
        public int? VersionDoctor { get; set; }
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public Status Status { get; set; }
       
        public int IdUser { get; set; }
        public Status StatusUser { get; set; }

        public int VersionUser { get; set; }
        public string Organisme { get; set; }

    }
}
