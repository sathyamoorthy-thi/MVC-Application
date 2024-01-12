using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ReimbursementClaim.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;

        // Constructor to inject IHttpContextAccessor
        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        // Action method to display the Welcome view
        public IActionResult Welcome()
        {
            return View();
        }

        // Action method to display the ContactUs view
        public IActionResult ContactUs()
        {
            return View();
        }

        // Action method to display the About view
        public IActionResult About()
        {
            return View();
        }

        // GET request action method for displaying the login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST request action method for handling login form submission
        [HttpPost]
        public IActionResult Login(UserCredentials user)
        {
            try
            {
                // Attempt to login the user using LoginUser.GetLogin method
                string result = LoginUser.GetLogin(user);

                if (result == "AdminSuccess")
                {
                    // Set session variables for an admin user
                    _contextAccessor.HttpContext.Session.SetString("username", user.username);
                    return RedirectToAction("AdminDashboard", "Home");
                }
                else if (result == "EmpSuccess")
                {
                    // Set session variables for an employee user
                    _contextAccessor.HttpContext.Session.SetString("username", user.username);
                    _contextAccessor.HttpContext.Session.SetString("mailid", user.emailAddress);
                    HttpContext.Session.SetString("user", user.username);

                    // Serialize and store user object in session
                    HttpContext.Session.SetObjectAsJson("logusers", user);
                    return RedirectToAction("ViewDetails", "Details");
                }

                ViewBag.Message = "Login fails, Try Again";
                return View("Login", user);
            }
            catch (UnauthorizedAccessException error)
            {
                Console.WriteLine("Access denied: " + error.Message);
                return View("Login", user);
            }
            catch (ApplicationException error)
            {
                ViewBag.ErrorMessage = "An error occurred during login: " + error.Message;
                return View("Error");
            }
        }
    }
}
