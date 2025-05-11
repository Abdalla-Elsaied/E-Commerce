using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Dtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        [Range(0.1,double.MaxValue,ErrorMessage ="Price Must be grater than Zero !!")]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be grater than Zero !!")]

        public int Quantity { get; set; }
    }
}