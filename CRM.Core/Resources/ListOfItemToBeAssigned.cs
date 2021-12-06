using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Resources
{
   public class ListOfItemToBeAssigned
    {
        public int Cycle { get; set; }

        public List<int> Users { get;set; }
    }
    public class ItemToBeAssigned
    {
        public int Cycle { get; set; }
        public int User { get; set; }

    }
}
