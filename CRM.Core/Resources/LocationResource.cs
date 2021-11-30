using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class LocationResource 
    {
        public int IdLocation { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? tel { get; set; }

        public int? UpdatedBy { get; set; }
        public int? IdLocationType { get; set; }
        public int VersionLocationType { get; set; }
        public Status StatusLocationType { get; set; }
        public string NameLocationType { get; set; }
        public string TypeLocationType { get; set; }
        public string Name { get; set; }



        public int IdLocality1 { get; set; }
        public int IdLocality2 { get; set; }
        public string NameLocality1 { get; set; }
        public string NameLocality2 { get; set; }
        public int IdBrick1 { get; set; }
        public int IdBrick2 { get; set; }
        public string NameBrick1 { get; set; }
        public string NameBrick2 { get; set; }
        public int NumBrick1 { get; set; }
        public int NumBrick2 { get; set; }


        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }

        public int? IdBrick { get; set; }
        public Status StatusBrick { get; set; }
        public int VersionBrick { get; set; }
        public string NameBrick { get; set; }

    }
}
