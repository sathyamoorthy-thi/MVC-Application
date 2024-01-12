// Import necessary namespaces
using System.Web.Mvc;
//using System.Web.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ReimbursementClaim;
using Serilog;
using ReimbursementClaim.Filter;
using Microsoft.AspNetCore.Authentication.Cookies;

// Create a new instance of WebApplicationBuilder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configure ExceptionFilter
// Configure global exception filter for controllers and views
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new MyExceptionFilters()); // Using Exception Filter Globally.
});
#endregion

// Add HttpClient service to the container
builder.Services.AddHttpClient();

// Add HttpContextAccessor service as a singleton
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region Configure Serilog
// Configure Serilog for logging
builder.Host.UseSerilog((context, config) => {
    config.WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day);

    if (context.HostingEnvironment.IsProduction() == false)
    {
        config.WriteTo.Console();
    }
});
#endregion

#region Configure Sessions
// Configure distributed memory cache for sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(5);
});
#endregion

#region Configure Database
// Configure database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
#endregion

#region Configure AuthorizeFilter
// Configure authentication with cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Login/Login";
            // Configure other cookie options if needed
        });
#endregion

#region Configure Filter
// Configure global filters for controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add<Filters>(); // Adding the Filters Class for using Filters globally.
});
#endregion

// Build the web application
var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    // Configure exception handling and HSTS
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Use session middleware
app.UseSession();

// Redirect to HTTPS
app.UseHttpsRedirection();

// Serve static files
app.UseStaticFiles();

// Use routing middleware
app.UseRouting();

// Use authorization middleware
app.UseAuthorization();

// Map default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Welcome}/{id?}");

// Start the application's request processing loop
app.Run();
