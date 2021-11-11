﻿using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class DoctorResource
    {
        public DoctorResource()
        {
            BuDoctor = new HashSet<BuDoctor>();
            CycleSectorWeekDoctors = new HashSet<CycleSectorWeekDoctorsResource>();
            EstablishmentDoctor = new HashSet<LocationDoctor>();
            Info = new HashSet<Info>();
            InverseLinked = new HashSet<Doctor>();
            Phone = new HashSet<Phone>();
            SpecialityDoctor = new HashSet<SpecialityDoctor>();
        }
        public int IdDoctor { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }

        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public int? NbPatientsDay { get; set; }
        public int? LinkedId { get; set; }
        public int VersionLink { get; set; }
        public Status StatusLink { get; set; }
        public int? ManagerApprouved { get; set; }

        public virtual Potentiel IdDoctorNavigation { get; set; }
        public virtual Doctor Linked { get; set; }
        public virtual ICollection<BuDoctor> BuDoctor { get; set; }
        public virtual ICollection<CycleSectorWeekDoctorsResource> CycleSectorWeekDoctors { get; set; }
        public virtual ICollection<LocationDoctor> EstablishmentDoctor { get; set; }
        public virtual ICollection<Info> Info { get; set; }
        public virtual ICollection<Doctor> InverseLinked { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
        public virtual ICollection<SpecialityDoctor> SpecialityDoctor { get; set; }
    }
}