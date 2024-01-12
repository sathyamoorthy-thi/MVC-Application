using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ReimbursementClaim;



namespace ReimbursementClaim
{

public class CustomAuthorizeFilter : Attribute , IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var users = context.HttpContext.Session.GetObjectFromJson<UserCredentials>("logusers");
        if (users == null)
        {
            Console.WriteLine("Error: users is null");
            context.Result = new RedirectToActionResult("", "", null);
            return;
        }
        Console.WriteLine($"users: {JsonConvert.SerializeObject(users)}");
    }
}
}