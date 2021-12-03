using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Resources
{
   public class ListOfItemToBeAssigned
    {
        public int IdOfItemToBeAssigned { get; set; }

        public List<int> IdsOfItemToBeAssignedToItem { get;set; }
    }
    public class ItemToBeAssigned
    {
        public int Item1 { get; set; }
        public int Item2 { get; set; }

    }
}
