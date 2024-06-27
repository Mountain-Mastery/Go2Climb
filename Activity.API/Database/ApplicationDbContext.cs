using Activity.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Activity.API.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
        : base(opt)
    {
        
    }

    public DbSet<Domain.Models.Activity> Activities { get; set; }
    public DbSet<Service> Services { get; set; }
}
