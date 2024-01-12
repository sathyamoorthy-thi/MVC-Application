using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReimbursementClaim
{
    public class DetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext _dbContext;

        public DetailsController(ApplicationDbContext paymentContext, ApplicationDbContext dbContext)
        {
            _context = paymentContext;
            _dbContext = dbContext;
        }

        public IActionResult Logout()
        {
            return View();
        }

        // This action method displays the EmployeeDetails view for GET requests
        [HttpGet]
        [CustomAuthorizeFilter] // Apply custom authorization filter
        public IActionResult EmployeeDetails()
        {
            return View();
        }

        // This action method processes the form submission for EmployeeDetails
        [HttpPost]
        [CustomAuthorizeFilter] // Apply custom authorization filter
        public IActionResult EmployeeDetails(ClaimDetails user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Status = "Pending";
                    _context.detail.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("Proof");
                }

                return View(user);
            }
            catch (Exception error)
            {
                // Handle errors and display an error view
                ViewBag.ErrorMessage = "An error occurred while processing the request: " + error.Message;
                Console.WriteLine("An error occurred while processing the request: " + error.Message);
                return View("Error");
            }
            finally
            {
                Console.WriteLine("Executed");
            }
        }

        [CustomAuthorizeFilter] // Apply custom authorization filter
        public IActionResult Proof()
        {
            return View();
        }

        // This action method handles file upload for Proof
        [HttpPost]
        [CustomAuthorizeFilter] // Apply custom authorization filter
        public async Task<IActionResult> Proof(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Please select a file");

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var image = new Proof
                    {
                        ImageData = memoryStream.ToArray()
                    };
                    _dbContext.proof.Add(image);
                    await _dbContext.SaveChangesAsync();
                }

                return View("Success");
            }
            catch (Exception error)
            {
                // Handle errors and display an error view
                ViewBag.ErrorMessage = "An error occurred while uploading the file: " + error.Message;
                Console.WriteLine("An error occurred while processing the request: " + error.Message);
                return View("Error");
            }
        }

        [CustomAuthorizeFilter] // Apply custom authorization filter
        public IActionResult ViewDetails()
        {
            try
            {
                string user = HttpContext.Session.GetString("user");
                List<ClaimDetails> claimDetails = _context.detail.Where(u => u.Name == user).ToList();
                return View("ViewDetails", claimDetails);
            }
            catch (Exception error)
            {
                // Handle errors and display an error view
                ViewBag.ErrorMessage = "An error occurred while retrieving the details: " + error.Message;
                Console.WriteLine("An error occurred while processing the request: " + error.Message);
                return View("Error");
            }
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }
    }
}
