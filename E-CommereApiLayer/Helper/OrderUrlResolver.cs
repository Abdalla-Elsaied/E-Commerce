using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Entities.Order_Aggregate;
using Talabat.API.Dtos;

namespace Talabat.API.Helper
{
    public class OrderUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.ProductUrl))
            {
                return $"{_configuration["ApiBase"]}/{source.Product.ProductUrl}";
            }
            return string.Empty;
        }
    }
}
