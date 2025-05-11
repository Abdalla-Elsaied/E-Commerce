using DomainLayer.Entities;
using DomainLayer.Entities.Order_Aggregate;
using DomainLayer.Repositories.Contract;
using DomainLayer.Services.Contract;
using DomainLayer.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceisLayer
{
    public class OrderService : IOrederService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository,
          IUniteOfWork uniteOfWork, IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _uniteOfWork = uniteOfWork;
            _paymentService = paymentService;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket =await _basketRepository.GetBasketAsync(basketId);

            var orderItems=new List<OrderItem>();
            if(basket?.Items?.Count>0)
            {
                foreach(var item in basket.Items)
                {
                    var product=await _uniteOfWork.Repository<Product>().GetAsync(item.Id);

                    var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                    
                    var orderItem=new OrderItem(productItemOrdered,product.Price,item.Quantity);

                    orderItems.Add(orderItem);
                }
            }

            var orderrepo =  _uniteOfWork.Repository<Order>();

            var subtotal=orderItems.Sum(orderItem=>orderItem.Price*orderItem.Quantity);

            var deliveryMethod = await _uniteOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);

            var orderspec = new GetOrderByPaymentIntentId(basket.PaymentIntentId);

            var existingOrder = await orderrepo.GetWithSpecAsync(orderspec);
            if (existingOrder != null)
            {
                orderrepo.Remove(existingOrder);

                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }

            var order =new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subtotal,basket.PaymentIntentId);

            await _uniteOfWork.Repository<Order>().AddAsync(order);

            int result= await _uniteOfWork.ComplateAsync();

            if(result<=0)return null;

            return order;

        }

        public async Task<IReadOnlyList<Order>?> GetOrdersForUserAsync(string buyerEmail)
        {
            var ordersRepo =  _uniteOfWork.Repository<Order>();
            var spec =new OrderSpec(buyerEmail);
            var orders = await ordersRepo.GetAllWithSpecAsync(spec);
            if (orders == null) return null;
            return orders;
        }
         
        public async Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var orderRepo = _uniteOfWork.Repository<Order>();
            var Spec = new OrderSpec(orderId, buyerEmail);
            var order=await orderRepo.GetWithSpecAsync(Spec);
             if (order == null) return null;
            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>?> GetDeliveryMethodsAsync()
        {
            var deliveryMethodRepo =_uniteOfWork.Repository<DeliveryMethod>();
            var deliveryMethods = deliveryMethodRepo.GetAllAsync();
            if (deliveryMethods == null) return null;
            return deliveryMethods;
        }
    }
}
