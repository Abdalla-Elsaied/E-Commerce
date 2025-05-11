using DomainLayer.Entities;
using DomainLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services.Contract
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdatePaymentIntentTOSucceededOrFailed(string paymentIntentId, bool IsSucceeded);
    }
}
