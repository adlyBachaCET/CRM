using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SaveVisitResource
    {

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }




        public int IdLocality1 { get; set; }


        public int IdLocality2 { get; set; }

        public int IdBrick1 { get; set; }
        public int IdBrick2 { get; set; }

        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }


        public int? IdDoctor { get; set; }

        public int? IdPharmacy { get; set; }




    }
}
