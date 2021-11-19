using CRM.Core.Models;
using System;
using System.Collections.Generic;

namespace CRM_API.Resources

{
    public class BrickResource
    {
 
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdBrick { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PostalCode { get; set; }
        public int NumSystemBrick { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? Active { get; set; }
    }
}
