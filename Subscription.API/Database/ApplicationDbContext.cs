using Microsoft.EntityFrameworkCore;

namespace Subscription.API.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
        : base(opt)
    {

    }
}
