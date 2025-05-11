using AutoMapper;
using DomainLayer.Entities.Order_Aggregate;
using DomainLayer.Repositories.Contract;
using DomainLayer.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.Dtos;
using Talabat.API.Errors;


namespace Talabat.API.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IOrederService _orederService;
        private readonly IMapper _mapper;

        public OrdersController(IBasketRepository basketRepository, IOrederService orederService, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _orederService = orederService;
            _mapper = mapper;
        }
        [HttpPost]
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderToReturnDto>> CreatOrder(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await _orederService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);
            if (order == null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>>GetOrdersForUser()
        {
            string BuyerEmail =User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orederService.GetOrdersForUserAsync(BuyerEmail);

            if (orders == null) return NotFound(new ApiResponse(404));
            
            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderToReturnDto>>GetOrderForUserById(int id)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var order= await _orederService.GetOrderByIdForUserAsync(id, email);
            if (order == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Order,OrderToReturnDto>(order));
        }

        [HttpGet("deliverymethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orederService.GetDeliveryMethodsAsync();
            if (deliveryMethods == null) return NotFound(new ApiResponse(404));
            return Ok(deliveryMethods);
        }
    }
}
