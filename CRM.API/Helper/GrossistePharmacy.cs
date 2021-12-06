using CRM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_API.Helper
{
    public class GrossistePharmacyStatus
    {
        public Status? Status { get; set; }
        public GrossistePharmacy GrossistePharmacy { get; set; }

    }
    public class GrossistePharmacyStatusById
    {
        public int Id { get; set; }


        public GrossistePharmacy GrossistePharmacy { get; set; }

    }
}
