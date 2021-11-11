using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class UserModel 
    {
        public UserModel()
        {
            BuUser = new HashSet<BuUser>();
            CycleSectorWeekDoctors = new HashSet<Target>();
        
            Phone = new HashSet<Phone>();
        }
        public int Version { get; set; }

        public int IdUserModel { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserModelType { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RegistrantionNumber { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<BuUser> BuUser { get; set; }
        public virtual ICollection<Target> CycleSectorWeekDoctors { get; set; }

        public virtual ICollection<Phone> Phone { get; set; }
    }
}
