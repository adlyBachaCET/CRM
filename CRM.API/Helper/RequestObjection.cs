using CRM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_API.Helper
{
    public class RequestObjectionStatus
    {
        public Status? Status { get; set; }
        public RequestObjection RequestObjection { get; set; }

    }
    public class RequestObjectionStatusById
    {
        public int Id { get; set; }


        public Status? Status { get; set; }
        public RequestObjection RequestObjection { get; set; }

    }
}
