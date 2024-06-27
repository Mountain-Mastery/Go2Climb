using Customer.API.Domain.Services.Communication;
using Customer.API.Domain.model;
using Go2Climb.API.Security.Domain.Services.Communication;

namespace Customer.API.Domain.Services;

public interface ICustomerService
{
    Task<IEnumerable<model.Customer>> ListAsync();
    Task<model.Customer> GetByIdAsync(int id);
    Task RegisterAsync(RegisterCustomerRequest request);
    Task UpdateAsync(int id, UpdateCustomerRequest request);
    Task DeleteAsync(int id);
    Task<CustomerResponse> FindById(int id);
}