namespace Subscription.API.Domain.Repository;

public interface ISubscriptionRepository
{
    Task<IEnumerable<Models.Subscription>> ListAsync();
    Task<Models.Subscription> FindByIdAsync(int id);
    Task<IEnumerable<Models.Subscription>> ListByName(string name);
    Models.Subscription FindById(int id);
    Task AddAsync(Models.Subscription subscription);
    void Update(Models.Subscription subscription);
    void Remove(Models.Subscription subscription);
}