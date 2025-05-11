using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
	public class Product:BaseModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string PictureUrl { get; set; }

		public int CategoryId { get; set; }
		public ProductCategory Category { get; set; }

		public int BrandId { get; set; }
		public ProductBrand Brand { get; set; }
	}
}
