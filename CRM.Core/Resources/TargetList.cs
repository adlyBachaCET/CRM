using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public partial class WeekUpdate
    {
        public int NumTarget { get; set; }

        public WeekContent NewContent { get; set; }

        public string Note { get; set; }
    }
    public partial class TargetList
    {
        public int NumTarget { get; set; }

        public List<WeekContent> WeekContents { get; set; }

        public string Note { get; set; }
    }
    public class WeekContent
    {
        public List<int?> IdDoctor { get; set; }
        public List<int?> IdPharmacy { get; set; }
        public int IdSector { get; set; }
    }
}
