using CRM.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_API.Helper
{
    public class AuthenticateUser
    {
        private readonly IUserService _UserService;

        public AuthenticateUser(IUserService UserService)
        {
            _UserService = UserService;

        }
        public string AhenticateToken(string Token)
        {
            try
            {
                var claims = _UserService.getPrincipal(Token);
                return true.ToString();
            }
            catch (Exception e)
            {
                return e.Message;

            }
        }
    }

}
