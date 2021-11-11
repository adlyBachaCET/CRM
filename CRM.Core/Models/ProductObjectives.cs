﻿using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class ProductSellingObjectives
    {
        public int IdProduct { get; set; }
        public Status StatusProduct { get; set; }

        public int IdSellingObjectives{ get; set; }
        public Status StatusSellingObjectives{ get; set; }
        public int Version { get; set; }
        public int VersionSellingObjectives{ get; set; }
        public int VersionProduct { get; set; }
        public int NameSellingObjectives{ get; set; }

        public virtual SellingObjectives IdSellingObjectivesNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public int Quantity { get; set; }
       
        
        public int? IdDoctor { get; set; }
   
        public int VersionDoctor { get; set; }
        public Status StatusDoctor { get; set; }
        public virtual Doctor Doctor { get; set; }


    }
}