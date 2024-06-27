using Subscription.API.Database;
using Subscription.API.Domain.Repository;
using Subscription.API.Domain.Services;
using Subscription.API.Domain.Services.Communication;
using Subscription.API.Resources;
using AutoMapper;

namespace Subscription.API.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper, ApplicationDbContext context)
    {
        _subscriptionRepository = subscriptionRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<Domain.Models.Subscription>> ListAsync()
    {
        return await _subscriptionRepository.ListAsync();
    }

    public async Task<IEnumerable<Domain.Models.Subscription>> ListByName(string name)
    {
        return await _subscriptionRepository.ListByName(name);
    }

    public async Task<Domain.Models.Subscription> GetByIdAsync(int id)
    {
        var subscription = await _subscriptionRepository.FindByIdAsync(id);
        if (subscription == null) throw new KeyNotFoundException("Subscription not found.");
        return subscription;
    }

    public async Task CreateAsync(SaveSubscriptionResource request)
    {
        var subscription = _mapper.Map<SaveSubscriptionResource, Domain.Models.Subscription>(request);

        try
        {
            await _subscriptionRepository.AddAsync(subscription);
             await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
             throw new Exception($"An error occurred while creating the subscription: {e.Message}");
        }
    }

    public async Task UpdateAsync(int id, UpdateSubscriptionRequest request)
    {
        var existingSubscription = await GetByIdAsync(id);

        _mapper.Map(request, existingSubscription);

        try
        {
             _subscriptionRepository.Update(existingSubscription);
             await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
             throw new Exception($"An error occurred while updating the subscription: {e.Message}");
        }
    }
        

    public async Task DeleteAsync(int id)
    {
        var subscription = GetById(id);

        try
        {
            _subscriptionRepository.Remove(subscription);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while deleting the subscription: {e.Message}");
        }
    }

    public async Task<SubscriptionResponse> FindById(int id)
    {
        var existingSubscription = await _subscriptionRepository.FindByIdAsync(id);

        if (existingSubscription == null)
            return new SubscriptionResponse("Subscription not found.");

        return new SubscriptionResponse(existingSubscription);
    }

    //Helper method
    private Domain.Models.Subscription GetById(int id)
    {
        var subscription = _subscriptionRepository.FindById(id);
        if (subscription == null) throw new KeyNotFoundException("Subscription not found.");
        return subscription;
    }
}
