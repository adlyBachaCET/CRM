using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class Planification
    {
        public int IdPlanification { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int Active { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
       
        public int Occurence{ get; set; }
        public DateTime DateStart{ get; set; }
        public DateTime DateEnd { get; set; }


        public string Description { get; set; }


    }
}
