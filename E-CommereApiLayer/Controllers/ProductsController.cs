using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Products_Spec;
using DomainLayer.Repositories.Contract;
using DomainLayer.Services.Contract;
using DomainLayer.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.API.Helper;
namespace Talabat.API.Controllers
{
	
	public class ProductsController : BaseController
	{
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController( IProductService productService, IMapper mapper)
		{
            _productService = productService;
            _mapper = mapper;
        }
		[CachAttribute(600)]
		[HttpGet]
		[ProducesResponseType(typeof(ProductReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IReadOnlyList<Product>>> GetProduct([FromQuery]ProductSpecParams ProductPrams)
		{

            var products = await _productService.GetProductsAsync(ProductPrams);
			var productDto = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductReturnDto>>(products);
			var count = await _productService.GetCountAsync(ProductPrams);
			Pagination<ProductReturnDto> DataPagination = new Pagination<ProductReturnDto>(ProductPrams.PageIndex , ProductPrams.PageSize,count, productDto);
			return Ok(DataPagination);

		}
		[HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
		{
           
            var product = await _productService.GetProductByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			var productDto= _mapper.Map<Product ,ProductReturnDto>(product);
			return Ok(productDto);
		}
		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
		{
			var brands = await _productService.GetBrandsAsync();
			return Ok(brands);
		}
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategories()
        {
            var categories = await _productService.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}

