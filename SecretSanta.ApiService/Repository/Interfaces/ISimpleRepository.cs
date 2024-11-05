namespace SecretSanta.ApiService.Repository.Interfaces;

public interface ISimpleRepository<T> where T : class
{
    Task<int> Add(T entity);
    Task Update(T entity);
    Task Delete(long id);
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(long id);
    Task<T?> Get(string id);
}
