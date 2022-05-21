using System.Linq.Expressions;
using Video.Domain.Entities;

namespace Video.Application.Interfaces;

public interface IRentalRepository : IAsyncGenericRepository<Rental>
{
    Task<IEnumerable<Rental>> GetAllRentalsAsync(params Expression<Func<Rental, object>>[] includeProperties);
    Task<Rental> GetRentalByIdAsync(int id);
    Task<Rental> DeleteRentalByIdAsync(int id);
}