using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class UpdateLocalityProfileUser
    {
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdLocality { get; set; }
        public string Name { get; set; }


    }
}
