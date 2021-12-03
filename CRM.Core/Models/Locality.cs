using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Locality 
    {
        public Locality()
        {
            InverseIdParentNavigation = new HashSet<Locality>();
            SectorLocality = new HashSet<SectorLocality>();
        }

        public int IdLocality { get; set; }
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int? IdParent { get; set; }
        public Status StatusParent { get; set; }
        public int VersionParent { get; set; }
        public int Lvl { get; set; }

        public Status Status { get; set; }

        public virtual Locality IdParentNavigation { get; set; }
        public virtual ICollection<Locality> InverseIdParentNavigation { get; set; }
        public virtual ICollection<SectorLocality> SectorLocality { get; set; }
        public virtual ICollection<User> User1 { get; set; }
        public virtual ICollection<User> User2 { get; set; }
        public virtual ICollection<Location> Location1 { get; set; }
        public virtual ICollection<Location> Location2 { get; set; }

        public virtual ICollection<Visit> Visit1 { get; set; }
        public virtual ICollection<Visit> Visit2 { get; set; }
        public virtual ICollection<Pharmacy> Pharmacy1 { get; set; }
        public virtual ICollection<Pharmacy> Pharmacy2 { get; set; }
    }
}
