﻿using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class DoctorResource
    {
   
        public int IdDoctor { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public int? NbPatientsDay { get; set; }
        public int? LinkedId { get; set; }
        public string Email { get; set; }

        public int VersionLink { get; set; }
        public Status StatusLink { get; set; }
        public int? ManagerApprouved { get; set; }
        public int IdSpecialty1 { get; set; }
        public string NameSpecialty1 { get; set; }
        public int IdSpecialty2 { get; set; }
        public string NameSpecialty2 { get; set; }
        public string Reference { get; set; }


        public int IdPotentiel { get; set; }
        public string NamePotentiel { get; set; }


    }
}
