using Microsoft.EntityFrameworkCore;
using ReimbursementClaim.Models;

namespace ReimbursementClaim
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
   //This constructor takes an instance of DbContextOptions<ApplicationDbContext> as a parameter and calls the base constructor of DbContext with those options.
   // This constructor is typically used when configuring the database connection and other options for the context.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  
        {
        }
  
        public DbSet<ClaimDetails> detail { get; set; }   //collection of entites for specific types
        public DbSet<Proof> proof { get; set; }
    }
}
