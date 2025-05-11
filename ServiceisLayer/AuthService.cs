using DomainLayer.Entities.Identity;
using DomainLayer.Services.Contract;
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

namespace ServiceisLayer
{
    public class AuthService : IAuthService
    {
        public IConfiguration _configuration { get; }
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<string> CreatTokenAsync(AppUser appUser,UserManager<AppUser> userManager)
        {
            //Private Claims 
            var authclims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,appUser.UserName),
                new Claim(ClaimTypes.Email,appUser.Email)
            };
            var Roles = await userManager.GetRolesAsync(appUser);
            foreach (var role in Roles)
            {
                authclims.Add(new Claim(ClaimTypes.Role, role));
            }

            //Create Key 
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            //Create Token 

            var Token = new JwtSecurityToken(
                audience:_configuration["JWT:ValidAudience"],
                issuer: _configuration["JWT:ValidIssuer"],
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:Duration"])),
                claims:authclims,
                signingCredentials:new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256Signature)
                );
            // generate Token 
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
