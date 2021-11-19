using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class UserResource
    {

        public int Version { get; set; }

        public int IdUser { get; set; }
        public Status Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserType UserType { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Email { get; set; }

        public int? RegistrantionNumber { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }



        public int? IdUserDirectManager { get; set; }
        public Status? StatusDirectManager { get; set; }
        public int? VersionDirectManager { get; set; }

        public int? IdUserDotlineManager1 { get; set; }
        public Status? StatusDotlineManager1 { get; set; }
        public int? VersionDotlineManager1 { get; set; }

        public int? IdUserDotlineManager2 { get; set; }
        public Status? StatusDotlineManager2 { get; set; }
        public int? VersionDotlineManager2 { get; set; }

        public int tel1 { get; set; }
        public int tel2 { get; set; }

        public string Note { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }
        public int IdLocality1 { get; set; }
        public Status StatusLocality1 { get; set; }
        public int VersionLocality1 { get; set; }
        public string NameLocality1 { get; set; }
        public int IdLocality2 { get; set; }
        public Status StatusLocality2 { get; set; }
        public int VersionLocality2 { get; set; }
        public string NameLocality2 { get; set; }





    }

}
