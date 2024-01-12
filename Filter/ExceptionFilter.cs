using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class MyExceptionFilters : ActionFilterAttribute, IExceptionFilter
{
    public void OnException(ExceptionContext filtercontext)
    {
        
       Exception error = filtercontext.Exception;

       filtercontext.ExceptionHandled=true; 

       filtercontext.Result = new ViewResult()
       {
              ViewName = "Exceptional"         //Redirecting the View page named Exceptional
       };
        
    }
} 