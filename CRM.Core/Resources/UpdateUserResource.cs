
using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class UpdateUserResource
    {

        public SaveUserResourceWithoutPasswordUpdate User{get;set;}
        public string ConfirmPassword { get; set; }
      
      

    }

}
