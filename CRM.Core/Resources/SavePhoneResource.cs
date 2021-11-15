using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SavePhoneResource 
    {

        public string Description { get; set; }


        public PhoneType PhoneType { get; set; }
        public string PhoneInfo { get; set; }
        public int PhoneNumber { get; set; }


    }

}
