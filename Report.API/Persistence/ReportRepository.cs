using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Report.API.Persistence.Contexts;
using Report.API.Persistence.Repositories;
using Report.API.Domain.Models;
using Report.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Report.API.Persistence
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        public ReportRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Report>> ListAsync()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<IEnumerable<Report>> ListByServiceId(int serviceId)
        {
            return await _context.Reports.Where(p => p.ServiceId == serviceId).Include(p => p.Customer).ToListAsync();
        }

        public async Task<IEnumerable<Report>> ListByCustomerId(int customerId)
        {
            return await _context.Reports.Where(p => p.CustomerId == customerId).Include(p => p.Customer).ToListAsync();
        }

        public async Task<Report> FindByIdAsync(int id)
        {
            return await _context.Reports
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Report report)
        {
            await _context.Reports.AddAsync(report);
        }

        public void Remove(Report report)
        {
            _context.Reports.Remove(report);
        }
    }
}