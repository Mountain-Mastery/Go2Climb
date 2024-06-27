using Activity.API.Database;
using Activity.API.Domain.Repository;
using Activity.API.Domain.Services;
using Activity.API.Domain.Services.Communication;
using Activity.API.Resources;
using AutoMapper;

namespace Activity.API.Services;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public ActivityService(IActivityRepository activityRepository, IMapper mapper, ApplicationDbContext context)
    {
        _activityRepository = activityRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<Domain.Models.Activity>> ListAsync()
    {
        return await _activityRepository.ListAsync();
    }

    public async Task<IEnumerable<Domain.Models.Activity>> ListByName(string name)
    {
        return await _activityRepository.ListByName(name);
    }

    public async Task<Domain.Models.Activity> GetByIdAsync(int id)
    {
        var activity = await _activityRepository.FindByIdAsync(id);
        if (activity == null) throw new KeyNotFoundException("Activity not found.");
        return activity;
    }

    public async Task CreateAsync(SaveActivityResource request)
    {
        var activity = _mapper.Map<SaveActivityResource, Domain.Models.Activity>(request);

        try
        {
            await _activityRepository.AddAsync(activity);
             await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
             throw new Exception($"An error occurred while creating the activity: {e.Message}");
        }
    }

    public async Task UpdateAsync(int id, UpdateActivityRequest request)
    {
        var existingActivity = await GetByIdAsync(id);

        _mapper.Map(request, existingActivity);

        try
        {
             _activityRepository.Update(existingActivity);
             await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
             throw new Exception($"An error occurred while updating the activity: {e.Message}");
        }
    }
        

    public async Task DeleteAsync(int id)
    {
        var activity = GetById(id);

        try
        {
            _activityRepository.Remove(activity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while deleting the activity: {e.Message}");
        }
    }

    public async Task<ActivityResponse> FindById(int id)
    {
        var existingActivity = await _activityRepository.FindByIdAsync(id);

        if (existingActivity == null)
            return new ActivityResponse("Activity not found.");

        return new ActivityResponse(existingActivity);
    }

    //Helper method
    private Domain.Models.Activity GetById(int id)
    {
        var activity = _activityRepository.FindById(id);
        if (activity == null) throw new KeyNotFoundException("Activity not found.");
        return activity;
    }
}
