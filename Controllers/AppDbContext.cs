using allocation.Models;
using Microsoft.EntityFrameworkCore;

namespace allocation.Controllers
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) { }

        public DbSet<Drivers> drivers { get; set; }


    }
}
