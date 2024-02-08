using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters
{
    public class ProductAuthorizationFilter(IProductService productService) : IAsyncActionFilter
    {
        private readonly IProductService ProductService = productService;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User != null)
            {
                var userId = context.HttpContext.User.Identity?.Name;

                int productId = (int)context.ActionArguments["id"];

                var product = await ProductService.GetProductByIdAsync(productId);

                if (product == null || product?.Creator != userId)
                {
                    // Access denied
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            await next(); // Continue to the action
        }
    }
}