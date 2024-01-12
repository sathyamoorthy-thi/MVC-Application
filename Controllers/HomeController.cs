using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReimbursementClaim.Models;

namespace ReimbursementClaim
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        // Constructor to inject ILogger and ApplicationDbContext
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext paymentContext)
        {
            _context = paymentContext;
            _logger = logger;
        }

        // Action method to retrieve and display data from an external API
        public async Task<IActionResult> Index()
        {
            try
            {
                // Create an HttpClient to make API requests
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                using (var client = new HttpClient(clientHandler))
                {
                    // Configure HttpClient
                    client.BaseAddress = new Uri("http://localhost:5156/api/Details/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Send GET request to external API
                    HttpResponseMessage response = await client.GetAsync("http://localhost:5156/api/Details/Get");

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var employee = JsonConvert.DeserializeObject<List<ClaimDetails>>(data);
                        return View(employee);
                    }
                    else
                    {
                        _logger.LogError("No Data has been found");
                        return View("Error");
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("An error occurred while processing the request: " + error.Message);
                ViewBag.ErrorMessage = "An error occurred while retrieving the data: " + error.Message;
                return View("Error");
            }
        }

        // Action method to display Approve view
        public IActionResult Approve()
        {
            return View();
        }

        // Action method to retrieve and display data from the database
        public async Task<IActionResult> Verify()
        {
            try
            {
                var verifyList = await _context.detail.ToListAsync();
                return View(verifyList);
            }
            catch (Exception error)
            {
                Console.WriteLine("An error occurred while processing the request: " + error.Message);
                ViewBag.ErrorMessage = "An error occurred while retrieving the data: " + error.Message;
                return View("Error");
            }
        }

        // Action method to display the Privacy view
        public IActionResult Privacy()
        {
            return View();
        }

        // Action method to handle GET request for editing a record
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var exist = await _context.detail.FirstOrDefaultAsync(x => x.Id == id);

                if (exist != null)
                {
                    return View(exist);
                }
                else
                {
                    return View("NotFound");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("An error occurred while processing the request: " + error.Message);
                ViewBag.ErrorMessage = "An error occurred while retrieving the data: " + error.Message;
                return View("Error");
            }
        }

        // Action method to handle POST request for editing a record
        [HttpPost]
        public async Task<IActionResult> Edit(ClaimDetails user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = await _context.detail.FirstOrDefaultAsync(x => x.Id == user.Id);
                    if (exist != null)
                    {
                        // Update fields and save changes
                        exist.Name = user.Name;
                        // ... (similarly update other fields)
                        await _context.SaveChangesAsync();
                        return RedirectToAction("AdminDashboard", "Home");
                    }
                    else
                    {
                        return View("NotFound");
                    }
                }
                catch (Exception error)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong: {error.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid model");
            return View(user);
        }

        // Action method to display AdminDashboard view
        public IActionResult AdminDashboard()
        {
            return View();
        }

        // Action methods to display claims based on their status
        public IActionResult Pending()
        {
            var pending = _context.detail.Where(p => p.Status == "Pending").ToList();
            return View(pending);
        }

        public IActionResult Rejected()
        {
            var rejected = _context.detail.Where(p => p.Status == "Rejected").ToList();
            return View(rejected);
        }

        public IActionResult Approved()
        {
            var approved = _context.detail.Where(p => p.Status == "Approved").ToList();
            return View(approved);
        }

        // Action method to handle errors
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
