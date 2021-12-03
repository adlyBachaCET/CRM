using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Location 
    {
   
        public int IdLocation { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? IdLocationType { get; set; }
        public int VersionLocationType { get; set; }
        public Status StatusLocationType { get; set; }
        public string NameLocationType { get; set; }
        public string TypeLocationType { get; set; }
        public string Name { get; set; }
        public int? Tel { get; set; }
        public virtual ICollection<LocationDoctor> LocationDoctor { get; set; }

        public virtual LocationType IdLocationTypeNavigation { get; set; }

        public int IdLocality1 { get; set; }
        public int VersionLocality1 { get; set; }
        public Status StatusLocality1 { get; set; }
        public virtual Locality Locality1 { get; set; }
        public string NameLocality1 { get; set; }


        public int IdLocality2 { get; set; }
        public int VersionLocality2 { get; set; }
        public Status StatusLocality2 { get; set; }
        public virtual Locality Locality2 { get; set; }
        public string NameLocality2 { get; set; }


        public int? IdBrick1 { get; set; }
        public int? VersionBrick1 { get; set; }
        public Status? StatusBrick1 { get; set; }
        public virtual Brick Brick1 { get; set; }
        public int? IdBrick2 { get; set; }
        public int? VersionBrick2 { get; set; }
        public Status? StatusBrick2 { get; set; }
        public virtual Brick Brick2 { get; set; }
        public string NameBrick1 { get; set; }
        public string NameBrick2 { get; set; }
        public int NumBrick1 { get; set; }
        public int NumBrick2 { get; set; }
        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }


        public int? LinkedId { get; set; }
        public int VersionLink { get; set; }
        public Status StatusLink { get; set; }
        public virtual Location Linked { get; set; }
        public virtual ICollection<Location> InverseLinked { get; set; }

    }
}
