using DomainLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;

namespace Talabat.API.Helper
{
    public class CachAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeLive;

        public CachAttribute(int TimeLive)
        {
            _timeLive = TimeLive;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var responseCachService =context.HttpContext.RequestServices.GetRequiredService<IResponseCachService>();
            //Ask ClR to creating object from ResponseCachService Explicitly 
            
            var cachKey=GenerateCachKeyFromRequest(context.HttpContext.Request);

            var response = await responseCachService.GetCashResponseAsync(cachKey);

            if (!string.IsNullOrEmpty(response))
            {
                var result = new ContentResult()
                {
                    Content = response,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = result;
                return;
            }

            var executingActionContext= await next.Invoke();

            if(executingActionContext.Result is OkObjectResult okObjectResult && okObjectResult.Value is not null)
            {
                await responseCachService.CashResponseAsync(cachKey, okObjectResult.Value,TimeSpan.FromSeconds(_timeLive));
            }
        }

        private string GenerateCachKeyFromRequest(HttpRequest request)
        {
            var keyBuilder=new StringBuilder();

            keyBuilder.Append(request.Path);

            foreach(var (key,value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
