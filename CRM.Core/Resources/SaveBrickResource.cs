using CRM.Core.Models;
using System;
using System.Collections.Generic;

namespace CRM_API.Resources
{
    public class SaveBrickResource
    {
   

        public string Name { get; set; }
        public string Description { get; set; }


        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
