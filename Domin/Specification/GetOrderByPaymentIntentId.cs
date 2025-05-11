using DomainLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specification
{
    public class GetOrderByPaymentIntentId:BaseSpecification<Order>
    {
        public GetOrderByPaymentIntentId(string paymentIntent)
            :base(O=>O.PaymentIntentId== paymentIntent)
        {
            
        }
    }
}
