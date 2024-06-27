

namespace Go2Climb.API.Security.Authorization.Handlers.Interfaces
{
    public interface IJwtHandler
    {
        public string GenerateToken(Customer.API.Domain.model.Customer customer);
        public int? ValidateToken(string token);
        
    }
}