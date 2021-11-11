using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class Appointement
    {
        public int IdAppointement { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }

        public DateTime Start { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int IdUser { get; set; }
        public Status StatusUser { get; set; }

        public int VersionsUser { get; set; }
        
        public virtual User User { get; set; }
        public int IdDoctor { get; set; }
        public Status StatusDoctor { get; set; }

        public int VersionsDoctor { get; set; }

        public virtual Doctor Doctor { get; set; }


    }
}
