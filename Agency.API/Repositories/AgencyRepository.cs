using Agency.API.Database;
using Agency.API.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Agency.API.Repositories;

public class AgencyRepository : IAgencyRepository
{
    private readonly ApplicationDbContext _context;

    public AgencyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Models.Agency>> ListAsync()
    {
        return await _context.Agencies.ToListAsync();
    }

    public async Task AddAsync(Domain.Models.Agency agency)
    {
        await _context.Agencies.AddAsync(agency);
    }

    public async Task<Domain.Models.Agency> FindByIdAsync(int id)
    {
        return await _context.Agencies.FindAsync(id);
    }

    public Domain.Models.Agency FindById(int id)
    {
        return _context.Agencies.Find(id);
    }

    public async Task<Domain.Models.Agency> FindByEmailAsync(string email)
    {
        return await _context.Agencies.SingleOrDefaultAsync(C => C.Email == email);
    }

    public bool ExistsByEmail(string email)
    {
        return _context.Agencies.Any(c => c.Email == email);
    }

    public async Task<IEnumerable<Domain.Models.Agency>> ListByName(string name)
    {
        return await _context.Agencies.Where(p => p.Name == name).ToListAsync();
    }

    public void Update(Domain.Models.Agency agency)
    {
        _context.Agencies.Update(agency);
    }

    public void Remove(Domain.Models.Agency agency)
    {
        _context.Agencies.Remove(agency);
    }
}
