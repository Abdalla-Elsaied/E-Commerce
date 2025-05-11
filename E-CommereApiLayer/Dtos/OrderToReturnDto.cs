using DomainLayer.Entities.Order_Aggregate;

namespace Talabat.API.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public string Status { get; set; }
        public Address ShippingAddress { get; set; }  //owned class
        public string DeliveryMethod { get; set; }  //navegation [one]
        public string DeliveryMethodCost { get; set; }  //navegation [one]
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public decimal Subtotal { get; set; }  // price of order without dilivery 

        public decimal Total { get; set; }  

        public string PaymentIntentId { get; set; } = " ";
    }
}
