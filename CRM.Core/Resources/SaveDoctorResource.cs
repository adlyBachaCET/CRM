using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class SaveDoctorResource
    {
  
        public int Version { get; set; }
        public Status Status { get; set; }

        public int Active { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public int? NbPatientsDay { get; set; }
        public int VersionLink { get; set; }
        public Status StatusLink { get; set; }
        public int? ManagerApprouved { get; set; }

        public virtual Potentiel Potentiel { get; set; }
        public virtual List<SaveBusinessUnitResource> BusinessUnits { get; set; }
        public virtual List<SaveLocationResource> Establishments { get; set; }
        public virtual List<SaveInfoResource> Infos { get; set; }
        public virtual List<SavePhoneResource> Phones { get; set; }
        public virtual List<SaveServiceResource> Services { get; set; }
        public virtual List<SaveSpecialtyResource> Specialtys { get; set; }
        public virtual ICollection<SaveTagsResource> TagsDoctor { get; set; }

    }
}
