using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class UpdatePasswordResource
    {


        public int IdUser { get; set; }
 
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }



    }

}
