using Activity.API.Domain.Services.Communication;
using Activity.API.Resources;

namespace Activity.API.Domain.Services;

public interface IActivityService
{
    Task<IEnumerable<Models.Activity>> ListAsync();
    Task<Models.Activity> GetByIdAsync(int id);
    Task CreateAsync(SaveActivityResource request);
    Task UpdateAsync(int id, UpdateActivityRequest request);
    Task DeleteAsync(int id);
    Task<ActivityResponse> FindById(int id);
    Task<IEnumerable<Models.Activity>> ListByName(string name);
}
