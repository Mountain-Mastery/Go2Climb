using Microsoft.EntityFrameworkCore;

namespace Report.API.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
        : base(opt)
    {

    }
}
