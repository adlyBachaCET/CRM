using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveLocationSelectResource
    {
        public int IdLocation { get; set; }

        public int Primary { get; set; }

        public int? IdService { get; set; }
        public int Order { get; set; }

        public bool ChefService { get; set; }
    }
}
