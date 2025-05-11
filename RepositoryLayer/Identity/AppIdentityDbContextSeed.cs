using DomainLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> _userManager)
        {
            if(_userManager.Users.Count()==0)
            {
               var user = new AppUser
                            {
                                DisplayName = "Abdalla Elsaied",
                                Email = "ahmed.nsr@linkdev.com",
                                UserName = "ahmed.nsr",
                                PhoneNumber = "01111111",
                            };
                await _userManager.CreateAsync(user, "Pa$$0rd");
            }
        }

    }
}
