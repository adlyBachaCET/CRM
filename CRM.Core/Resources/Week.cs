using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public partial class Week
    {
        public int NumTarget { get; set; }

        public CycleResource Cycle { get; set; }
        public List<PotentielResource> PotentielResourceCycle { get; set; }

        public UserResource User { get; set; }

        public string Note { get; set; }
        public List<WeekTarget> WeekTarget { get; set; }
    }
    public partial class WeekTarget
    {
        public List<PharmacyResource> Pharmacys { get; set; }
        public List<DoctorResource> Doctors { get; set; }
        public SectorResource Sector { get; set; }
        public PotentielTotal PotentielTotal { get; set; }

    }
    public partial class PotentielTotal
    {
        public List<PotentielResource> PotentielResourceSector { get; set; }
        public List<PotentielTotalSector> PotentielTotalSector { get; set; }


    }
    public partial class PotentielTotalSector
    {
        public string Name { get; set; }
        public int Total { get; set; }
        public int NB { get; set; }

    }

    public partial class WeekSwap
    {
        public int NumTarget { get; set; }


        public int IdSector1 { get; set; }
        public int IdSector2 { get; set; }


    }
    public partial class WeekDeletion
    {
        public int NumTarget { get; set; }

        public int IdSector1 { get; set; }



    }
  
}
