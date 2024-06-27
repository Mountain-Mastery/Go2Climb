using Agency.API.Domain.Services.Communication;
using Agency.API.Resources;

namespace Agency.API.Domain.Services;

public interface IAgencyService
{
    Task<IEnumerable<Models.Agency>> ListAsync();
    Task<Models.Agency> GetByIdAsync(int id);
    Task RegisterAsync(SaveAgencyResource request);
    Task UpdateAsync(int id, UpdateAgencyRequest request);
    Task DeleteAsync(int id);
    Task<AgencyResponse> FindById(int id);

    Task<IEnumerable<Models.Agency>> ListByName(string name);
}
