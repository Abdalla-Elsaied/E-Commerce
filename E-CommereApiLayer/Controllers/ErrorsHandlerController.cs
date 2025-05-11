using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Data;
using Talabat.API.Errors;

namespace Talabat.API.Controllers
{
  
    public class ErrorsHandlerController : BaseController
    {
        private readonly StoreContext _appContext;

        public ErrorsHandlerController(StoreContext appContext)
        {
            _appContext = appContext;
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            var product = _appContext.Products.Find(100);
            if (product is null) return NotFound(new ApiResponse(404));
            return Ok(product);
        }
        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            var product = _appContext.Products.Find(100);
            if (product is null)
            {
                var productToreturn = product.ToString();
            }
            return Ok(product);
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
