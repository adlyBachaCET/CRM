using System;
using System.Collections.Generic;

namespace CRM.Core.Models
{
    public partial class UserResource 
    {
        public UserResource()
        {
            BuUser = new HashSet<BuUser>();
            CycleSectorWeekDoctors = new HashSet<CycleSectorWeekDoctors>();
            DelegateManagerIdDelegateNavigation = new HashSet<DelegateManager>();
            DelegateManagerIdManagerNavigation = new HashSet<DelegateManager>();
            EstablishmentUser = new HashSet<EstablishmentUser>();
            Phone = new HashSet<Phone>();
        }
        public int Version { get; set; }

        public int IdUser { get; set; }
        public int Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RegistrantionNumber { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<BuUser> BuUser { get; set; }
        public virtual ICollection<CycleSectorWeekDoctors> CycleSectorWeekDoctors { get; set; }
        public virtual ICollection<DelegateManager> DelegateManagerIdDelegateNavigation { get; set; }
        public virtual ICollection<DelegateManager> DelegateManagerIdManagerNavigation { get; set; }
        public virtual ICollection<EstablishmentUser> EstablishmentUser { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
    }

}
