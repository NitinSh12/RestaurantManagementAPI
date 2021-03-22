using AuthenticationAuthorizationData.DataModels;
using AuthenticationAuthorizationData.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationSql
{
    public class AuthenticationAuthoricationRepo: IAuthenticationAuthorizationRepo
    {
        private readonly UserManager<ApplicationUserDataModel> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationAuthoricationRepo(UserManager<ApplicationUserDataModel> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async ValueTask<bool> areValidCrendtials(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var isValidUser = await _userManager.CheckPasswordAsync(user, password);

            if (user != null && isValidUser)
                return true;
            
            return false;
        }

        public async ValueTask<bool> isUserExists(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
                return true;

            return false;
        }

        public async ValueTask<string> GenerateToken(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> Register(ApplicationUserDataModel user,string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            return result;
        }
    }
}
