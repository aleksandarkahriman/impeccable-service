using Logger.Abstraction;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace ImpeccableService.Backend.API
{
    public class UnhandledExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                return;
            }

            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<UnhandledExceptionFilter>>();
            logger.Error(context.Exception, "Unhandled exception.");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
