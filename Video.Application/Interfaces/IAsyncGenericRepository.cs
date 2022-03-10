namespace Video.Application.Interfaces;

public interface IAsyncGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
}
