using Subscription.API.Domain.Services.Communication;
using Subscription.API.Resources;

namespace Subscription.API.Domain.Services;

public interface ISubscriptionService
{
    Task<IEnumerable<Models.Subscription>> ListAsync();
    Task<Models.Subscription> GetByIdAsync(int id);
    Task CreateAsync(SaveSubscriptionResource request);
    Task UpdateAsync(int id, UpdateSubscriptionRequest request);
    Task DeleteAsync(int id);
    Task<SubscriptionResponse> FindById(int id);
    Task<IEnumerable<Models.Subscription>> ListByName(string name);
}
