using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class WholeSaler 
    {
        public WholeSaler()
        {
            WholeSalerLocality = new HashSet<WholeSalerLocality>();
        }
        public int Version { get; set; }

        public int IdPwholeSaler { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<WholeSalerLocality> WholeSalerLocality { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }

    }
}
