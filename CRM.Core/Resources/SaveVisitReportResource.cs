using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SaveVisitReportResource
    {
        public int IdReport { get; set; }

  
   
        public int IdVisit { get; set; }

        public string VisiteType { get; set; }

        public string Accompaniement { get; set; }
        public string InfoAccompaniement { get; set; }
        public string Competitiveintelligence { get; set; }
        public string Commentary { get; set; }
        public bool Replacement { get; set; }
        public string Name_Replacement { get; set; }
        public string Objectif { get; set; }
        public string ObjectifNextVisit { get; set; }

        public string CycleTtype { get; set; }



    }
}
