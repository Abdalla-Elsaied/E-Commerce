using DomainLayer;
using DomainLayer.Entities.Identity;
using DomainLayer.Repositories.Contract;
using DomainLayer.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Identity;
using RepositoryLayer.Repository;
using ServiceisLayer;
using StackExchange.Redis;
using Talabat.API.Errors;
using Talabat.API.Helper;

namespace Talabat.API.Extentions
{
    public static class ApplicationServicesExtension
    {
        
        public static IServiceCollection AddApplicationServicers (this IServiceCollection services)
        {
            services.AddSingleton(typeof(IResponseCachService),typeof(ResponseCachService));
            services.AddScoped(typeof(IUniteOfWork),typeof(UniteOfWork)) ;
            services.AddScoped(typeof(IOrederService), typeof(OrderService));
            services.AddScoped(typeof(IProductService), typeof(ProductService)) ;
            services.AddScoped(typeof(IPaymentService), typeof(PaymentService)) ;
            services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            services.AddScoped<ImageUrlResolver>();
            services.AddScoped<OrderUrlResolver>();
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();
            #region Validation Configuer
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                          .SelectMany(p => p.Value.Errors)
                                                          .Select(p => p.ErrorMessage)
                                                          .ToArray();
                    var validationResponse = new ApiValidationResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationResponse);
                };
            });
            #endregion
            return services;
        }
    }
}
