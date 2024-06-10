using Microsoft.EntityFrameworkCore;

namespace Customer.API.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
        : base(opt)
    {

    }
}
