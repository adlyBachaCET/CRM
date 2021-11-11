using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Locality 
    {
        public Locality()
        {
            BrickLocality = new HashSet<BrickLocality>();
            InverseIdParentNavigation = new HashSet<Locality>();
            SectorLocality = new HashSet<SectorLocality>();
            WholeSalerLocality = new HashSet<WholeSalerLocality>();
        }

        public int IdLocality { get; set; }
        public int Version { get; set; }

        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int? IdParent { get; set; }
        public Status StatusParent { get; set; }
        public int VersionParent { get; set; }

        public Status Status { get; set; }

        public virtual Locality IdParentNavigation { get; set; }
        public virtual ICollection<BrickLocality> BrickLocality { get; set; }
        public virtual ICollection<Locality> InverseIdParentNavigation { get; set; }
        public virtual ICollection<SectorLocality> SectorLocality { get; set; }
        public virtual ICollection<WholeSalerLocality> WholeSalerLocality { get; set; }
        public virtual ICollection<AdresseLocality> AdresseLocality { get; set; }

    }
}
