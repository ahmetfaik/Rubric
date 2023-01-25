using Microsoft.AspNetCore.Mvc.Filters;
using Rubric.Scheduler.Api.Filters.ValidationModel;

namespace Rubric.Scheduler.Api.Filters;

public class ValidationFilter : ActionFilterAttribute
{
    public void OnActionExecution(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new ValidationFailedResult(context.ModelState);
        }
    }
}