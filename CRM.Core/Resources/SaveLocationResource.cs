using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveLocationResource 
    {
        public LocationAdd LocationDetails { get; set; }



        public List<SaveServiceResource> ServiceList { get; set; }
    }

    public class LocationAdd
    {


        public int? IdLocationType { get; set; }


        public string Name { get; set; }


        public int IdLocality1 { get; set; }
        public int? tel { get; set; }


        public int IdLocality2 { get; set; }




        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }
        public int? IdBrick1 { get; set; }
        public int? IdBrick2 { get; set; }
    }
    public class OrderLocation
    {
        public int Order { get; set; }
        public int Primary { get; set; }
    }
    public class Chief
    {
        public bool ChiefService { get; set; }
    }
}
