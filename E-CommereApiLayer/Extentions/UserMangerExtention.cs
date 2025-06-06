﻿using DomainLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Talabat.API.Extentions
{
    public static class UserMangerExtention
    {
        public static async Task<AppUser> FindAddressByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
             var email = User.FindFirstValue(ClaimTypes.Email);
             
            var user= await userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u => u.Email == email);

            return user;
             
        }
    }
}
