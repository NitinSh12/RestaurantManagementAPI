using AuthenticationAuthorizationData.DataModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationData.Interfaces
{
    public interface IAuthenticationAuthorizationRepo
    {
        public ValueTask<bool> areValidCrendtials(string userName, string password);
        public ValueTask<bool> isUserExists(string userName);
        public ValueTask<string> GenerateToken(string userName);
        public Task<IdentityResult> Register(ApplicationUserDataModel user, string password);

    }
}
