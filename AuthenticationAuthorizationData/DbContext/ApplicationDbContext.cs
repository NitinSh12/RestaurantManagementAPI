using AuthenticationAuthorizationData.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationAuthorizationData.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUserDataModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
