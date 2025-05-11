using DomainLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specification
{
    public class OrderSpec : BaseSpecification<Order>
    {
        public OrderSpec(string buyerEmail)
            : base(O => O.BuyerEmail == buyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDes(O => O.OrderDate);
        }
        public OrderSpec(int OrderId, string BuyerEmail)
            : base(O => O.BuyerEmail == BuyerEmail && O.Id == OrderId)
        {
            
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            
        }
    }
}
