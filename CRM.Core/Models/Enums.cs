using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Models
{
    public enum Status
    {
        Approuved,
        Rejected,
        Pending,
        Statisfied,
        NotSatisfied,

    }
    public enum UserType
    {
        Manager,
        Delegue
    }
    public enum RequestObjection
    {
        Request, Objection
    }
}
