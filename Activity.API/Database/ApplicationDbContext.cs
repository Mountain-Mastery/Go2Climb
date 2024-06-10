using Microsoft.EntityFrameworkCore;

namespace Activity.API.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
        : base(opt)
    {
        
    }
}
