namespace Agency.API.Domain.Repository;

public interface IAgencyRepository
{
    Task<IEnumerable<Models.Agency>> ListAsync();
    Task<Models.Agency> FindByIdAsync(int id);
    Task<IEnumerable<Models.Agency>> ListByName(string name);
    Models.Agency FindById(int id);
    Task AddAsync(Models.Agency agency);
    void Update(Models.Agency agency);
    void Remove(Models.Agency agency);
    Task<Models.Agency> FindByEmailAsync(string email);
    public bool ExistsByEmail(string email);
}
