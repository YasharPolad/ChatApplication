using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Slacker.Api.Contracts;

namespace Slacker.Api.Filters;

public class ValidateInput : ActionFilterAttribute, IActionFilter
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        
    }

    
}
