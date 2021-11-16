using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class MessageUser 
    {
        public int IdUser1 { get; set; }
        public Status StatusUser1 { get; set; }
        public int VersionUser1 { get; set; }
        public int IdUser2 { get; set; }
        public Status StatusUser2 { get; set; }
        public int VersionUser2 { get; set; }
        public int IdMessage { get; set; }
        public Status StatusMessage { get; set; }
        public int VersionMessage { get; set; }
        public int NameMessage { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public int Version { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
        public virtual Message Message { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }

    }
}
