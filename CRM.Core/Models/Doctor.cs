using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class Doctor 
    {
        public Doctor()
        {
            BuDoctor = new HashSet<BuDoctor>();
            CycleSectorWeekDoctors = new HashSet<Target>();
            Info = new HashSet<Info>();
            InverseLinked = new HashSet<Doctor>();
            Phone = new HashSet<Phone>();
        }
        public int IdDoctor { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<SellingObjectives> SellingObjectives { get; set; }
        public virtual ICollection<Participant> Participant { get; set; }

        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string Reference { get; set; }

        public int? NbPatientsDay { get; set; }
        public int? LinkedId { get; set; }
        public int VersionLink { get; set; }
        public Status StatusLink { get; set; }
        public int? ManagerApprouved { get; set; }

        public virtual Doctor Linked { get; set; }
        public virtual ICollection<BuDoctor> BuDoctor { get; set; }
        public virtual ICollection<Target> CycleSectorWeekDoctors { get; set; }
        public virtual ICollection<Info> Info { get; set; }
        public virtual ICollection<Doctor> InverseLinked { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
        public int IdSpecialty1{ get; set; }
        public string NameSpecialty1 { get; set; }

      
        public virtual ICollection<TagsDoctor> TagsDoctor { get; set; }
        public int IdSpecialty2 { get; set; }
        public string NameSpecialty2 { get; set; }


        public int IdPotentiel { get; set; }
        public string NamePotentiel { get; set; }


        public virtual ICollection<SharedFiles> SharedFiles { get; set; }




    }
}
