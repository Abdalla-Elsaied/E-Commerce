using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Order_Aggregate
{
    public class Order:BaseModel
    {

        public Order() { } 
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal,string paymentintentid)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
            PaymentIntentId = paymentintentid;
        }

        public  string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }=OrderStatus.Pending;
        public Address ShippingAddress { get; set; }  //owned class
        public DeliveryMethod? DeliveryMethod { get; set; }  //navegation [one]
        public ICollection<OrderItem> Items { get; set; }=new HashSet<OrderItem>();
        public decimal Subtotal {  get; set; }  // price of order without dilivery 

        public decimal GetTotal()=>Subtotal+DeliveryMethod.Cost;  //derivitave properity 

        public string PaymentIntentId { get; set; }

    }
}
