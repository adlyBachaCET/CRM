
using System;
using System.Collections.Generic;
namespace CRM.Core.Models
{
    public partial class SaveUserResource 
    {

        public int Version { get; set; }

        public Status Status { get; set; }

        public int Active { get; set; }

        public DateTime? BirthDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RegistrantionNumber { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        public int? IdUserDirectManager { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? IdUserDotlineManager1 { get; set; }

        public int? IdUserDotlineManager2 { get; set; }

        public int tel1 { get; set; }
        public int tel2 { get; set; }

        public string Note { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }
        public int? IdLocality1 { get; set; }
        public string NameLocality1 { get; set; }
        public int? IdLocality2 { get; set; }
        public string NameLocality2 { get; set; }

        public int? IdLocality3 { get; set; }
        public string NameLocality3 { get; set; }


    }

}
