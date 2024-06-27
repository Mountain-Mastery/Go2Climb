using Subscription.API.Domain.Services;
using Subscription.API.Resources;
using Subscription.API.Services;
using AutoMapper;

namespace Subscription.API.Controllers;

public static class SubscriptionController
{
    public static void AddSubscriptionEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/subscriptions", async (IMapper mapper, ISubscriptionService subscriptionService) =>
        {
            var subscriptions = await subscriptionService.ListAsync();
            var resources = mapper.Map<IEnumerable<Domain.Models.Subscription>, IEnumerable<SubscriptionResource>>(subscriptions);
            return Results.Ok(resources);
        });

        app.MapGet("api/v1/subscriptions/{id}", async (int id, IMapper mapper, ISubscriptionService subscriptionService) =>
        {
            var subscription = await subscriptionService.GetByIdAsync(id);
            if (subscription == null)
            {
                return Results.NotFound(new { message = "Subscription not found" });
            }
            var resources = mapper.Map<Domain.Models.Subscription, SubscriptionResource>(subscription);
            return Results.Ok(resources);
        });

        app.MapPost("api/v1/subscriptions", async (SaveSubscriptionResource request, ISubscriptionService subscriptionService) =>
        {
            await subscriptionService.CreateAsync(request);
            return Results.Ok(new { message = "Registration successful" });
        });

        app.MapPut("api/v1/subscriptions/{id}", async (int id, UpdateSubscriptionRequest request, ISubscriptionService subscriptionService) =>
        {
            var subscription = await subscriptionService.GetByIdAsync(id);
            if (subscription == null)
            {
                return Results.NotFound(new { message = "Subscription not found" });
            }
            await subscriptionService.UpdateAsync(id, request);
            return Results.Ok(new { message = "Subscription updated successfully" });
        });

        app.MapDelete("api/v1/subscriptions/{id}", async (int id, ISubscriptionService subscriptionService) =>
        {
            var subscription = await subscriptionService.GetByIdAsync(id);
            if (subscription == null)
            {
                return Results.NotFound(new { message = "Subscription not found" });
            }
            await subscriptionService.DeleteAsync(id);
            return Results.Ok(new { message = "Subscription deleted successfully" });
        });
    }
}
