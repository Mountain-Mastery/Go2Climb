using Activity.API.Domain.Services;
using Activity.API.Resources;
using Activity.API.Services;
using AutoMapper;

namespace Activity.API.Controllers;

public static class ActivityController
{
    public static void AddActivityEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/activities", async (IMapper mapper, IActivityService activityService) =>
        {
            var activities = await activityService.ListAsync();
            var resources = mapper.Map<IEnumerable<Domain.Models.Activity>, IEnumerable<ActivityResource>>(activities);
            return Results.Ok(resources);
        });

        app.MapGet("api/v1/activities/{id}", async (int id, IMapper mapper, IActivityService activityService) =>
        {
            var activity = await activityService.GetByIdAsync(id);
            if (activity == null)
            {
                return Results.NotFound(new { message = "Activity not found" });
            }
            var resources = mapper.Map<Domain.Models.Activity, ActivityResource>(activity);
            return Results.Ok(resources);
        });

        app.MapPost("api/v1/activities", async (SaveActivityResource request, IActivityService activityService) =>
        {
            await activityService.CreateAsync(request);
            return Results.Ok(new { message = "Registration successful" });
        });

        app.MapPut("api/v1/activities/{id}", async (int id, UpdateActivityRequest request, IActivityService activityService) =>
        {
            var activity = await activityService.GetByIdAsync(id);
            if (activity == null)
            {
                return Results.NotFound(new { message = "Activity not found" });
            }
            await activityService.UpdateAsync(id, request);
            return Results.Ok(new { message = "Activity updated successfully" });
        });

        app.MapDelete("api/v1/activities/{id}", async (int id, IActivityService activityService) =>
        {
            var activity = await activityService.GetByIdAsync(id);
            if (activity == null)
            {
                return Results.NotFound(new { message = "Activity not found" });
            }
            await activityService.DeleteAsync(id);
            return Results.Ok(new { message = "Activity deleted successfully" });
        });
    }
}
