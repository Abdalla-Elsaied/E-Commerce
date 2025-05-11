using DomainLayer.Entities;
using DomainLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepositoryLayer.Data
{
	public static class StoreContextSeed
	{
		public async static Task SeedAsync(StoreContext _dbcontext)
		{

			if (_dbcontext.Brands.Count() == 0)
			{
				var brandsData = File.ReadAllText("../RepositoryLayer/Data/DataSeed/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

				if (brands?.Count > 0)
				{
					foreach (var brand in brands)
					{
						_dbcontext.Set<ProductBrand>().Add(brand);
					}
					await _dbcontext.SaveChangesAsync();
				}
			}
			if (_dbcontext.Categories.Count() == 0)
			{
				var CategoryData = File.ReadAllText("../RepositoryLayer/Data/DataSeed/categories.json");
				var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryData);
				if (Categories?.Count > 0)
				{
					foreach (var category in Categories)
					{
						_dbcontext.Set<ProductCategory>().Add(category);
					}
					await _dbcontext.SaveChangesAsync();
				}
			}
			if (_dbcontext.Products.Count() == 0)
			{
				var productData = File.ReadAllText("../RepositoryLayer/Data/DataSeed/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productData);

				if (products?.Count > 0)
				{
					{
						foreach (var product in products)
						{
							_dbcontext.Set<Product>().Add(product);
						}
						await _dbcontext.SaveChangesAsync();
					}

				}

			}
            if (_dbcontext.DeliveryMethods.Count() == 0)
            {
                var DeliveryData = File.ReadAllText("../RepositoryLayer/Data/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);

                if (deliveryMethods?.Count > 0)
                {
                    {
                        foreach (var method in deliveryMethods)
                        {
                            _dbcontext.Set<DeliveryMethod>().Add(method);
                        }
                        await _dbcontext.SaveChangesAsync();
                    }

                }

            }
        }
	}
}
