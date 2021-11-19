using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CRM_API.Helper
{
    public class Token
    {
        public string TokenString { get; set; }
    }
    public class TokenPassword
    {
        public string ConfirmPassword { get; set; }

        public string NewPassword { get; set; }
    }

}

