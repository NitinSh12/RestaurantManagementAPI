using AuthenticationAuthorizationDomain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationDomain.Interfaces
{
    public interface IAuthenticationAuthorizationService
    {
        public ValueTask<bool> areValidCrendtials(LoginModel model);
        public ValueTask<bool> isUserExists(string userName);
        public ValueTask<string> GenerateToken(string userName);
        public Task<IdentityResult> Register(RegisterModel model);
    }
}
