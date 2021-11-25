using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public class TargetByNumber
    {
        public CycleResource Cycle { get; set; }
        public UserResource User { get; set; }
        public List<TargetResource> TargetResources { get; set; }
        public List<PotentielResource> PotentielResources { get; set; }
        public List<PotentielResource> PotentielSectorResources { get; set; }
    }
}
