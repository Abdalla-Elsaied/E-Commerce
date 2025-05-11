using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Repositories.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.API.Errors;

namespace Talabat.API.Controllers
{
    
    public class CartController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public CartController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>>  GetBasket(string id)
        {
           var basket=  await _basketRepository.GetBasketAsync(id);
           // return null because of expired of basket then create new basket 
           return Ok(basket??new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var customerBasket=_mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var Updated = await _basketRepository.UpdateBasketAsync(customerBasket);
            if (Updated is null) return BadRequest(new ApiResponse(400));
            return Ok(Updated);
        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
