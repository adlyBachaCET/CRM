using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class SaveCycleUserResource
    {

   
        public int IdCycle { get; set; }
        public int IdUser { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
