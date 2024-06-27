using System.Collections.Generic;
using System.Threading.Tasks;
using Report.API.Domain.Models;
using Report.API.Domain.Services.Communication;

namespace Report.API.Domain.Services
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> ListAsync();
        Task<IEnumerable<Report>> ListByServiceIdAsync(int serviceId);
        Task<IEnumerable<Report>> ListByCustomerIdAsync(int customerId);
        Task<ReportResponse> GetByIdAsync(int id);
        Task<ReportResponse> SaveAsync(Report report);
        Task<ReportResponse> DeleteAsync(int id);
    }
}