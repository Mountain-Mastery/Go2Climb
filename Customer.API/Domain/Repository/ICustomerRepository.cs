namespace Customer.API.Domain.Repository;

public interface ICustomerRepository
{
    Task<IEnumerable<model.Customer>> ListAsync();
    Task AddAsync(model.Customer customer);
    Task<model.Customer> FindByIdAsync(int id);
    model.Customer FindById(int id);
    Task<model.Customer> FindByEmailAsync(string email);
    public bool ExistsByEmail(string email);
    void Update(model.Customer customer);
    void Remove(model.Customer customer);
}