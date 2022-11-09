using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Slacker.Api.Contracts;

namespace Slacker.Api.Filters;

public class ValidateInput : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
       if(!context.ModelState.IsValid)
        {
            var result = new ErrorResponse();
            context.ModelState.Values.SelectMany(v => v.Errors).ToList().ForEach(error => result.Errors.Add(error.ErrorMessage));
            context.Result = new BadRequestObjectResult(result);
        }
    }

    
}
