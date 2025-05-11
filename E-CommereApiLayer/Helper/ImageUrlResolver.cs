using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using DomainLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using Talabat.API.Dtos;

namespace Talabat.API.Helper
{
    public class ImageUrlResolver : IValueResolver<Product, ProductReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ImageUrlResolver( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiBase"]}/{source.PictureUrl}";
            }
            return string.Empty ;
        }
     
    }
}
