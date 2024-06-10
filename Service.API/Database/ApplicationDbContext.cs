using Microsoft.EntityFrameworkCore;

namespace Service.API.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
        : base(opt)
    {

    }
}
