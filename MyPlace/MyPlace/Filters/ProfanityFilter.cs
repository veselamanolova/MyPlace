
namespace MyPlace.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ProfanityFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }
    }
}

