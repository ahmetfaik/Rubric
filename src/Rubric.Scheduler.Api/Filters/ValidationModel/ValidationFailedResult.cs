using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Rubric.Scheduler.Api.Filters.ValidationModel;

public class ValidationFailedResult : ObjectResult
{
    public ValidationFailedResult(ModelStateDictionary modelState) : base(new ValidationErrorResponse(modelState))
    {
        StatusCode = StatusCodes.Status400BadRequest;
    }
}