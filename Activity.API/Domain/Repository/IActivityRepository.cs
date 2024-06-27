namespace Activity.API.Domain.Repository;

public interface IActivityRepository
{
    Task<IEnumerable<Models.Activity>> ListAsync();
    Task<Models.Activity> FindByIdAsync(int id);
    Task<IEnumerable<Models.Activity>> ListByName(string name);
    Models.Activity FindById(int id);
    Task AddAsync(Models.Activity activity);
    void Update(Models.Activity activity);
    void Remove(Models.Activity activity);
}