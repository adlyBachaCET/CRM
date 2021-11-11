using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
   public  class Message
    {
        public int IdMessage { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public virtual ICollection<MessageUser> MessageUser { get; set; }


    }
}
