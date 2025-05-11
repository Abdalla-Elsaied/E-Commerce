using DomainLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services.Contract
{
    public interface IAuthService
    {
       public Task<string> CreatTokenAsync(AppUser appUser,UserManager<AppUser> userManager);
    }
}
