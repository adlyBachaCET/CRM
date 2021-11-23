
using System;
using System.Collections.Generic;
using System.Text;
namespace CRM.Core.Models
{
    public class AffectationCycleUser
    {

        public SaveCycleResource SaveCycleResource { get; set; }
        public List<int> Ids { get; set; }
        public List<CyclePotentiel> CyclePotentiel { get; set; }

    }
    public class CyclePotentiel
    {
        public int IdPotentiel { get; set; }
        public int Frequence { get; set; }

    }
}
