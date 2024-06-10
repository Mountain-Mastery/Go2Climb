using Agency.API.Database;
using Microsoft.EntityFrameworkCore;

namespace Agency.API.Extentions;

public static class MigrationExtention
{
    public static void AddMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}
