﻿using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveBusinessUnitResource 
    {


        public int Version { get; set; }
        public int Active { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}
