using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveInfoResource
    {

        public int Version { get; set; }

        public int Active { get; set; }
        public string Datatype { get; set; }
        public string TypeInfo { get; set; }


        public string Data { get; set; }

    }
}
