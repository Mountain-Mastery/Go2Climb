using Subscription.API.Database;
using Subscription.API.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Subscription.API.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Models.Subscription>> ListAsync()
    {
        return await _context.Subscriptions.ToListAsync();
    }

    public async Task AddAsync(Domain.Models.Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
    }

    public async Task<Domain.Models.Subscription> FindByIdAsync(int id)
    {
        return await _context.Subscriptions.FindAsync(id);
    }

    public Domain.Models.Subscription FindById(int id)
    {
        return _context.Subscriptions.Find(id);
    }

    public async Task<IEnumerable<Domain.Models.Subscription>> ListByName(string name)
    {
        return await _context.Subscriptions.Where(p => p.Name == name).ToListAsync();
    }

    public void Update(Domain.Models.Subscription subscription)
    {
        _context.Subscriptions.Update(subscription);
    }

    public void Remove(Domain.Models.Subscription subscription)
    {
        _context.Subscriptions.Remove(subscription);
    }
}
