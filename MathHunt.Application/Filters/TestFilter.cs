using Microsoft.AspNetCore.Mvc.Filters;

namespace MathHunt.Application.Filters;

public class TestFilter : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var actionArguments = context.HttpContext.Request.Cookies;
        var obj = actionArguments["token"];
        // foreach (var item in actionArguments)
        // {
        //     var value = $"Key: {item.Key}\n Value: {item.Value}";
        // }
        var executedContext = await next();
        var result = executedContext.Result;
    }
}