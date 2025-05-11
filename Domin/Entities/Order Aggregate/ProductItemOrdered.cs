namespace DomainLayer.Entities.Order_Aggregate
{
    public class ProductItemOrdered: BaseModel
    {
        public ProductItemOrdered() { }
        public ProductItemOrdered(int productId, string productName, string productUrl)
        {
            ProductId = productId;
            ProductName = productName;
            ProductUrl = productUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
    }
}