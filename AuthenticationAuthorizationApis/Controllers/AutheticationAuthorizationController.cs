using AuthenticationAuthorizationDomain.Interfaces;
using AuthenticationAuthorizationDomain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationApis.Controllers
{
    public class AutheticationAuthorizationController : ControllerBase
    {
        private readonly IAuthenticationAuthorizationService _authenticationAuthorizationService;

        public AutheticationAuthorizationController(IAuthenticationAuthorizationService authenticationAuthorizationService)
        {
            _authenticationAuthorizationService = authenticationAuthorizationService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            var areValidCrenditals = await _authenticationAuthorizationService.areValidCrendtials(model);
            
            if(areValidCrenditals)
            {
                var result = await _authenticationAuthorizationService.GenerateToken(model.UserName);
                return Ok(new
                {
                    token = result
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var isUserExists = await _authenticationAuthorizationService.isUserExists(model.Username);

            if (isUserExists)
                return StatusCode(StatusCodes.Status500InternalServerError, new ServiceResponseModel { Status = "Error", Message = "User already exists!" });

            var result = await _authenticationAuthorizationService.Register(model);

            if(!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ServiceResponseModel { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new ServiceResponseModel { Status = "Success", Message = "User created successfully!" });
        }
    }
}
