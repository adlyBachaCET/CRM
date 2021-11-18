using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Resources
{
   public class Link
    {
        public int Parent { get; set; }
        public List<int> Childs { get;set; }
    }
}
