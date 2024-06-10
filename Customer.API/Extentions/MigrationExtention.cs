﻿using Customer.API.Database;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Extentions;

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
