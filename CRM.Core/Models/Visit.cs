using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class Visit
    {
        public int IdVisit { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<VisitUser> VisitUser { get; set; }



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
        public int VersionBrick1 { get; set; }
        public Status StatusBrick1 { get; set; }
        public virtual Brick Brick1 { get; set; }
        public int? IdBrick2 { get; set; }
        public int VersionBrick2 { get; set; }
        public Status StatusBrick2 { get; set; }
        public virtual Brick Brick2 { get; set; }

        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }


        public int? IdDoctor { get; set; }
        public int VersionDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<VisitFileTracking> VisitFileTracking { get; set; }




    }
}
