namespace Video.Application.Interfaces;

public interface IAsyncGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
}
