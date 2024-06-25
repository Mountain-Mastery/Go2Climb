using Agency.API.Domain.Services;
using Agency.API.Resources;
using Agency.API.Services;
using AutoMapper;

namespace Agency.API.Controllers;

public static class AgencyController
{
    public static void AddAgencyEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/agencies", async (IMapper mapper, IAgencyService agencyService) =>
        {
            var agencies = await agencyService.ListAsync();
            var resources = mapper.Map<IEnumerable<Domain.Models.Agency>, IEnumerable<AgencyResource>>(agencies);
            return Results.Ok(resources);
        });

        app.MapGet("api/v1/agencies/{id}", async (int id, IMapper mapper, IAgencyService agencyService) =>
        {
            var agency = await agencyService.GetByIdAsync(id);
            var resources = mapper.Map<Domain.Models.Agency, AgencyResource>(agency);
            return Results.Ok(resources);
        });

        app.MapPost("api/v1/agencies/auth/sing-up", async (SaveAgencyResource request, IAgencyService agencyService) =>
        {
            await agencyService.RegisterAsync(request);
            return Results.Ok(new { message = "Registration successful" });
        });

        app.MapPut("api/v1/agencies/{id}", async (int id, UpdateAgencyRequest request, IAgencyService agencyService) =>
        {
            await agencyService.UpdateAsync(id, request);
            return Results.Ok(new { message = "Agency updated successfully" });
        });

        app.MapDelete("api/v1/agencies/{id}", async (int id, IAgencyService agencyService) =>
        {
            await agencyService.DeleteAsync(id);
            return Results.Ok(new { message = "Agency deleted successfully" });
        });
    }
}
