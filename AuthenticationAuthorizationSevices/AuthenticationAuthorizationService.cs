using AuthenticationAuthorizationData.DataModels;
using AuthenticationAuthorizationData.Interfaces;
using AuthenticationAuthorizationDomain.Interfaces;
using AuthenticationAuthorizationDomain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationSevices
{
    public class AuthenticationAuthorizationService: IAuthenticationAuthorizationService
    {
        private readonly IAuthenticationAuthorizationRepo _authenticationAuthorizationRepo;

        public AuthenticationAuthorizationService(IAuthenticationAuthorizationRepo authenticationAuthorizationRepo)
        {
            _authenticationAuthorizationRepo = authenticationAuthorizationRepo;
        }

        public async ValueTask<bool> areValidCrendtials(LoginModel model)
        {
            return await _authenticationAuthorizationRepo.areValidCrendtials(model.UserName, model.Password);
        }

        public async ValueTask<string> GenerateToken(string userName)
        {
            return await _authenticationAuthorizationRepo.GenerateToken(userName);
        }

        public async ValueTask<bool> isUserExists(string userName)
        {
            return await _authenticationAuthorizationRepo.isUserExists(userName);
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var applicationUser = new ApplicationUserDataModel
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            return await _authenticationAuthorizationRepo.Register(applicationUser, model.Password);
        }
    }
}
