using Microsoft.EntityFrameworkCore;
using Subscription.API.Database;

namespace Subscription.API.Extentions;

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
