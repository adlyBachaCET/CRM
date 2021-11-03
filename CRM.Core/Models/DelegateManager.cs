using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class DelegateManager 
    {
        public int IdDelegate { get; set; }
        public Status StatusDelegate { get; set; }
        public int VersionDelegate { get; set; }

        public int IdManager { get; set; }
        public Status StatusManager { get; set; }
        public int VersionManager { get; set; }
        public int Version { get; set; }

        public string TypeRelation { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Status Status { get; set; }
        public virtual User IdDelegateNavigation { get; set; }
        public virtual User IdManagerNavigation { get; set; }
    }
}
