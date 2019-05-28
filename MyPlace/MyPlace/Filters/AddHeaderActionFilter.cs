
namespace MyPlace.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;

    public class AddHeaderActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context) =>
            context.HttpContext.Response.Headers
                .Add("X-Debug", context.HttpContext.User?.Identity?.Name
                ?? "Unauthorized!");
    }
}

// [TypeFilter(typeof(AddHeaderActionFilter))]