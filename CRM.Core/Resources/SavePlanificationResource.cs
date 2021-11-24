using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SavePlanificationResource
    {

       
        public int Occurence{ get; set; }
        public DateTime DateStart{ get; set; }
        public DateTime DateEnd { get; set; }
        public string Description { get; set; }


    }
}
