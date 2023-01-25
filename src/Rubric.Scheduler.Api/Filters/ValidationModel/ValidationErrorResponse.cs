using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Rubric.Scheduler.Api.Filters.ValidationModel;

public class ValidationErrorResponse
{
    public ValidationErrorResponse(ModelStateDictionary modelState)
    {
        Message = "Validation Failed";
        Errors = modelState.Keys
            .SelectMany(s => modelState[s]!.Errors.Select(t => new ValidationError(s, t.ErrorMessage)))
            .ToList();
    }
    
    public string Message { get; }
    public List<ValidationError> Errors { get; }
}