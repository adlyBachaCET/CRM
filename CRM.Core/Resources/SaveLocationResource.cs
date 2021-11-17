﻿using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveLocationResource 
    {


        public int? IdLocationType { get; set; }

  
        public string Name { get; set; }
        public int? Primary { get; set; }

     
        public int IdLocality1 { get; set; }
        public string NameLocality1 { get; set; }
        public int? tel { get; set; }


        public int IdLocality2 { get; set; }

        public string NameLocality2 { get; set; }



        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }

        public int IdBrick { get; set; }
   
        public string NameBrick { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public List<SaveServiceResource> SaveServiceResource { get; set; }
    }
}
