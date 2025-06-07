using Inventory_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Backend.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Inventorydata> Inventory { get; set; }
    }
}
