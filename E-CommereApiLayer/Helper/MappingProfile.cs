using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Entities.Identity;
using DomainLayer.Entities.Order_Aggregate;
using Talabat.API.Dtos;

namespace Talabat.API.Helper
{
    public class MappingProfile:Profile  
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductReturnDto>()
                .ForMember(d => d.Brand, O => O.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, O => O.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ImageUrlResolver>());
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, DomainLayer.Entities.Order_Aggregate.Address>();
            CreateMap<DomainLayer.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, S => S.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(D => D.DeliveryMethodCost, O => O.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.ProductUrl, O => O.MapFrom(S => S.Product.ProductUrl))
                .ForMember(D=>D.ProductUrl,O=>O.MapFrom<OrderUrlResolver>());
                

        }
    }
}
