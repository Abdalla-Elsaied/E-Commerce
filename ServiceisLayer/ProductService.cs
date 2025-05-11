using DomainLayer.Entities;
using DomainLayer.Products_Spec;
using DomainLayer.Repositories.Contract;
using DomainLayer.Services.Contract;
using DomainLayer.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceisLayer
{
    public class ProductService : IProductService
    {
        private readonly IUniteOfWork _uniteOfWork;

        public ProductService(IUniteOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams ProductPrams)
        {
            var spec = new ProductWithBrandandCategorySpec(ProductPrams);
            var products = await _uniteOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            return products;
        }
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            var spec = new ProductWithBrandandCategorySpec(productId);
            var product = await _uniteOfWork.Repository<Product>().GetWithSpecAsync(spec);
            return product;
        }
        public async Task<int> GetCountAsync(ProductSpecParams ProductPrams)
        {
            var CountSpec = new ProductWithFilterationForCountSpec(ProductPrams);
            var count = await _uniteOfWork.Repository<Product>().GetCountSpecAsync(CountSpec);
            return count;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        => await _uniteOfWork.Repository<ProductBrand>().GetAllAsync();

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
        =>await _uniteOfWork.Repository<ProductCategory>().GetAllAsync();

   
    }
}
