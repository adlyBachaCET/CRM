using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class FavouriteFiles
    {
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdFile { get; set; }
        public int VersionFile { get; set; }
        public Status StatusFile { get; set; }
        public virtual File File { get; set; }

        public int IdUser { get; set; }
        public int VersionUser { get; set; }
        public Status StatusUser { get; set; }
        public virtual User User { get; set; }

    }
}
