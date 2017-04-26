using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Interfaces.Common;

namespace WordHunt.WebAPI.Filters
{
    public class SetValidResponseFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context is null && !(context.Result is ObjectResult))
                return;

            var result = (context.Result as ObjectResult)?.Value as RequestResult;
            if (result != null)
            {
                if (!result.IsSuccess)
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}
