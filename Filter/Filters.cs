using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ReimbursementClaim.Filter
{
    //[AttributeUsage(AttributeTargets.Method)]
    public class Filters : IActionFilter, IResultFilter
    {
    private readonly ILogger<Filters> _logger;

    public Filters(ILogger<Filters> logger)
    {
      _logger = logger;
    }
      public void OnActionExecuted(ActionExecutedContext context)
        {
        
        _logger.LogInformation("Executing Action filter - OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
         _logger.LogInformation("Executing Action filter - OnActionExecuting");   
        }

      
        public void OnResultExecuted(ResultExecutedContext context)
        {
          _logger.LogInformation("Executing Result filter - OnResultExecuted");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
          _logger.LogInformation("Executing Result filter - OnResultExecuting");
        }
    }
}