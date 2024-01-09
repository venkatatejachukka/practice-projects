using CRUDusingwebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDusingwebapi.DAL
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
