using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveSpecialtyResource
    {

        public int Version { get; set; }

        public int Active { get; set; }
        public string Name { get; set; }
        public string Abreviation { get; set; }
        public Status Status { get; set; }
    }
}
