using DomainLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services.Contract
{
    public interface IOrederService
    {
        Task<Order?> CreateOrderAsync(string buyerEmail,string basketId ,int deliveryMethodId,Address shippingAddress);
        Task<IReadOnlyList<Order>?> GetOrdersForUserAsync(string buyerEmail);
        Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>?> GetDeliveryMethodsAsync();
    }
}
