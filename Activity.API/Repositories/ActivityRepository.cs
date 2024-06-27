using Activity.API.Database;
using Activity.API.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Activity.API.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly ApplicationDbContext _context;

    public ActivityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Models.Activity>> ListAsync()
    {
        return await _context.Activities.ToListAsync();
    }

    public async Task AddAsync(Domain.Models.Activity activity)
    {
        await _context.Activities.AddAsync(activity);
    }

    public async Task<Domain.Models.Activity> FindByIdAsync(int id)
    {
        return await _context.Activities.FindAsync(id);
    }

    public Domain.Models.Activity FindById(int id)
    {
        return _context.Activities.Find(id);
    }

    public async Task<IEnumerable<Domain.Models.Activity>> ListByName(string name)
    {
        return await _context.Activities.Where(p => p.Name == name).ToListAsync();
    }

    public void Update(Domain.Models.Activity activity)
    {
        _context.Activities.Update(activity);
    }

    public void Remove(Domain.Models.Activity activity)
    {
        _context.Activities.Remove(activity);
    }
}
