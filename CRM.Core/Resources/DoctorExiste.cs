using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{


    public partial class DoctorExiste
    {
        public Doctor FirstLast { get; set; }
        public bool FirstLastExist { get; set; }
        public Doctor LastFirst { get; set; }
        public bool LastFirstExist { get; set; }

        public Doctor DoctorEmail { get; set; }
        public bool ExistDoctorEmail{ get; set; }
    }
    public partial class DoctorExisteResource
    {
        public DoctorResource FirstLast { get; set; }
        public bool FirstLastExist { get; set; }
        public DoctorResource LastFirst { get; set; }
        public bool LastFirstExist { get; set; }

        public DoctorResource DoctorEmail { get; set; }
        public bool ExistDoctorEmail { get; set; }
    }
    public partial class VerifyDoctor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
