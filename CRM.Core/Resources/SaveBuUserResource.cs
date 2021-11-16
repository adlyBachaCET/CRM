using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveBuUserResource 
    {
        public int IdUser { get; set; }

        public int IdBu { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
