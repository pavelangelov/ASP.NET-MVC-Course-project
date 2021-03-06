﻿using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using Bg_Fishing.Models;

namespace Bg_Fishing.Auth
{
    public class ApplicationUser : AppUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
