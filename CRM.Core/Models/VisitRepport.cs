using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class VisitReport
    {
        public int IdReport { get; set; }

        public int Version { get; set; }
        public Status Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
   
        public int IdVisit { get; set; }
        public int VersionVisit { get; set; }
        public Status StatusVisit { get; set; }
        public virtual Visit Visit { get; set; }
        public string VisiteType { get; set; }

        public string Accompaniement { get; set; }
        public string InfoAccompaniement { get; set; }
        public string Competitiveintelligence { get; set; }
        public string Commentaire { get; set; }
       // public string Competitiveintelligence { get; set; }

        public string Objectif { get; set; }
        public string ObjectifNextVisit { get; set; }

        public string CycleTtype { get; set; }
        public ICollection<VisitRequestReport> VisitRequestReport { get; set; }

        public ICollection<ProductSampleVisitReport> ProductSampleVisitReport { get; set; }

        public ICollection<ProductVisitReport> ProductVisitReport { get; set; }


    }
}
