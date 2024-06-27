using AutoMapper;
using Customer.API.Domain.Repository;
using Customer.API.Domain.Services;
using Customer.API.Domain.Services.Communication;
using Go2Climb.API.Security.Authorization.Handlers.Interfaces;
using Go2Climb.API.Security.Domain.Services.Communication;
using Go2Climb.API.Security.Exceptions;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Go2Climb.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        
        public CustomerService(ICustomerRepository customerRepository, IJwtHandler jwtHandler, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Customer.API.Domain.model.Customer>> ListAsync()
        {
            return await _customerRepository.ListAsync();
        }
        
        public async Task<Customer.API.Domain.model.Customer> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.FindByIdAsync(id);
            if (customer == null) throw new KeyNotFoundException("Customer not found.");
            return customer;
        }

        public async Task RegisterAsync(RegisterCustomerRequest request)
        {
            //Validate
            if (_customerRepository.ExistsByEmail(request.Email))
                throw new AppException($"Email {request.Email} is already taken.");
            
            //Map request to customer
            var customer = _mapper.Map<Customer.API.Domain.model.Customer>(request);
            
            //Hash Password
            customer.PasswordHash = BCryptNet.HashPassword(request.Password);
            
            //Save customer
            //Console.WriteLine($"{customer}");
            try
            {
                await _customerRepository.AddAsync(customer);
            }
            catch (Exception e)
            {
                throw new AppException($"An error occurred while saving the customer: {e.Message}");
            }
        }

        public async Task UpdateAsync(int id, UpdateCustomerRequest request)
        {
            var customer = GetById(id);
            
            //Validate
            if(_customerRepository.ExistsByEmail(request.Email)) 
                throw new AppException($"Email {request.Email} is already taken.");
            
            //Hash Password if entered
            if (!string.IsNullOrEmpty(request.Password))
                customer.PasswordHash = BCryptNet.HashPassword(request.Password);
            
            //Map request to Customer
            _mapper.Map(request, customer);
            
            try
            {
                _customerRepository.Update(customer);
            }
            catch (Exception e)
            {
                throw new AppException($"An error occurred while updating the customer: {e.Message}");
            }
        }

        public async Task<CustomerResponse> FindById(int id)
        {
            var existingCustomer = await _customerRepository.FindByIdAsync(id);

            if (existingCustomer == null)
                return new CustomerResponse("Customer not found.");

            return new CustomerResponse(existingCustomer);
        }

        public async Task DeleteAsync(int id)
        {
            var customer = GetById(id);

            try
            {
                _customerRepository.Remove(customer);
            }
            catch (Exception e)
            {
                throw new AppException($"An error occurred while deleting the customer: {e.Message}");
            }
        }
        
        //Helper method
        private Customer.API.Domain.model.Customer GetById(int id)
        {
            var customer = _customerRepository.FindById(id);
            if (customer == null) throw new KeyNotFoundException("Customer not found.");
            return customer;
        }
    }
}