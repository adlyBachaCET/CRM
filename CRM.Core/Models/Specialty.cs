using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Specialty
    {
   
        public int Version { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int IdSpecialty { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Name { get; set; }
        public string Abreviation { get; set; }
        public Status Status { get; set; }
    }
}
