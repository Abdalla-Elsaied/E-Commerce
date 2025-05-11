using DomainLayer.Entities;
using DomainLayer.Entities.Order_Aggregate;
using DomainLayer.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.V2;
using Talabat.API.Errors;

namespace Talabat.API.Controllers
{
    [Authorize]
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;
        private const string _whSecret = "whsec_49969bb89802114c99d89100b5c539a120eef6929cea76d3f524bad89eb0246b";
        public PaymentsController(IPaymentService paymentService,ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("{CartId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket=await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket == null) return BadRequest(new ApiResponse(400, "An Error with your Basket"));
            return Ok(basket);
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _whSecret);

                var paymenintent = (PaymentIntent)stripeEvent.Data.Object;

                DomainLayer.Entities.Order_Aggregate.Order order;
                switch (stripeEvent.Type)
                {
                    case "payment.intent.succeeded":
                        order = await _paymentService.UpdatePaymentIntentTOSucceededOrFailed(paymenintent.Id, true);
                        _logger.LogInformation("payment is succeeded ",paymenintent.Id);
                        break;
                    case "payment.intent.failed":
                        order = await _paymentService.UpdatePaymentIntentTOSucceededOrFailed(paymenintent.Id, false);
                        _logger.LogInformation("payment is falid",paymenintent.Id);
                        break;
                }
                return new EmptyResult();
        }
    }
}
