using DomainLayer.Entities;
using DomainLayer.Entities.Order_Aggregate;
using DomainLayer.Repositories.Contract;
using DomainLayer.Services.Contract;
using DomainLayer.Specification;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order = DomainLayer.Entities.Order_Aggregate.Order;
using Product = DomainLayer.Entities.Product;

namespace ServiceisLayer
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUniteOfWork _uniteOfWork;

        public PaymentService(IConfiguration configuration,IBasketRepository basketRepository,IUniteOfWork uniteOfWork)
        {
             _configuration = configuration;
            _basketRepository = basketRepository;
            _uniteOfWork = uniteOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettingkey:Secretkey"];
            var basket =await _basketRepository.GetBasketAsync(basketId);

            if (basket == null) return null;

            var shippingPrice = 0m;

            if(basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _uniteOfWork.Repository<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
                basket.ShippingPrice=deliveryMethod.Cost;
            }

            if(basket?.Items.Count>0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _uniteOfWork.Repository<Product>().GetAsync(item.Id);
                    if(item.Price!=product.Price)
                    {
                        item.Price = product.Price;
                    }
                }
            }
            PaymentIntentService  paymentIntentService = new PaymentIntentService(); 
            PaymentIntent paymentIntent;
            if(string.IsNullOrEmpty(basket.PaymentIntentId)) // create paymentintent
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100)+(long)shippingPrice,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" },
                };

                paymentIntent= await paymentIntentService.CreateAsync(option);

                basket.PaymentIntentId= paymentIntent.Id;
                basket.ClientSecret= paymentIntent.ClientSecret;
            }
            else  //update paymentintent
            {
                var UpdateOptions = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)shippingPrice,
                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, UpdateOptions);
            }

            await _basketRepository.UpdateBasketAsync(basket);

            return basket;

        }

        public async Task<Order> UpdatePaymentIntentTOSucceededOrFailed(string id, bool IsSucceeded)
        {
            var orderspec = new GetOrderByPaymentIntentId(id);
            var order = await _uniteOfWork.Repository<Order>().GetWithSpecAsync(orderspec);
            
            if(IsSucceeded)
                order.Status=OrderStatus.PaymentSuccessed;
            else 
                order.Status=OrderStatus.PaymentFailed;

             _uniteOfWork.Repository<Order>().UpdateAsync(order);

            await _uniteOfWork.ComplateAsync();

            return order;
        }
    }
}
