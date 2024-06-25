using Agency.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Agency.API.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
        : base(opt)
    {

    }

    public DbSet<Domain.Models.Agency> Agencies { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<AgencyReview> AgencyReviews { get; set; }
}
