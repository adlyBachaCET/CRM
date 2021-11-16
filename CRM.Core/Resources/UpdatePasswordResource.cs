using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class UpdatePasswordResource
    {

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }



    }

}
