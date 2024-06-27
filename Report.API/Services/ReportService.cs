using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Report.API.Domain.Repositories;
using Report.API.Domain.Models;
using Report.API.Domain.Repositories;
using Report.API.Domain.Services;
using Report.API.Domain.Services.Communication;

namespace Report.API.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IReportRepository reportRepository, ICustomerRepository customerRepository, IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
        {
            _reportRepository = reportRepository;
            _customerRepository = customerRepository;
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Report>> ListAsync()
        {
            return await _reportRepository.ListAsync();
        }

        public async Task<IEnumerable<Report>> ListByServiceIdAsync(int serviceId)
        {
            return await _reportRepository.ListByServiceId(serviceId);
        }

        public async Task<IEnumerable<Report>> ListByCustomerIdAsync(int customerId)
        {
            return await _reportRepository.ListByCustomerId(customerId);
        }

        public async Task<ReportResponse> GetByIdAsync(int id)
        {
            var existingResource = _reportRepository.FindByIdAsync(id);
            if (existingResource.Result == null)
                return new ReportResponse("The report does not exist.");
            
            return new ReportResponse(existingResource.Result);
        }

        public async Task<ReportResponse> SaveAsync(Report report)
        {
            var existingCustomer = _customerRepository.FindByIdAsync(report.CustomerId);
            if (existingCustomer == null)
                return new ReportResponse("Customer does not exist.");
            var exitingService = _serviceRepository.FindById(report.ServiceId);
            if (exitingService == null)
                return new ReportResponse("Service does not exist.");
            try
            {
                await _reportRepository.AddAsync(report);
                await _unitOfWork.CompleteAsync();
                
                var reportsOfService = await _reportRepository.ListByServiceId(report.ServiceId);
                var numberOfReports = reportsOfService.Count();
                if (numberOfReports == 5)
                {
                    foreach (var reportOfService in reportsOfService)
                    {
                        await DeleteAsync(reportOfService.Id);
                    }
                    
                    _serviceRepository.Remove(exitingService.Result);
                    await _unitOfWork.CompleteAsync();
                    return new ReportResponse("Service has been deleted for getting 5 reports.");
                }
                
                await _unitOfWork.CompleteAsync();

                return new ReportResponse(report);
            }
            catch (Exception e)
            {
                return new ReportResponse($"An error occurred while saving the report: {e.Message}");
            }
        }

        public async Task<ReportResponse> DeleteAsync(int id)
        {
            //Validate Report
            var existingReport = await _reportRepository.FindByIdAsync(id);

            if (existingReport == null)
                return new ReportResponse("Service review not found.");

            try
            {
                _reportRepository.Remove(existingReport);
                await _unitOfWork.CompleteAsync();

                return new ReportResponse(existingReport);
            }
            catch (Exception e)
            {
                return new ReportResponse($"An error occurred while deleting the service review: {e.Message}");
            }
        }
    }
}