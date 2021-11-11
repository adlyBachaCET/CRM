﻿using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class BuUser 
    {
        public int IdUser { get; set; }
        public Status StatusUser { get; set; }
        public int VersionUser { get; set; }

        public virtual User IdUserNavigation { get; set; }

        public int IdBu { get; set; }
        public Status StatusBu { get; set; }
        public int VersionBu { get; set; }

        public int NameBu { get; set; }

        public int Version { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
        public virtual BusinessUnit IdBuNavigation { get; set; }
    }
}
