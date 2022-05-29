using System.Linq.Expressions;
using Video.Domain.Entities;

namespace Video.Application.Interfaces;

public interface IRentalRepository : IAsyncGenericRepository<Rental>
{
    Task<IEnumerable<Rental>> GetAllRentalsAsync(Expression<Func<Rental, bool>>? filter = null, params Expression<Func<Rental, object>>[] includeProperties);
    Task<Rental> GetRentalByIdAsync(int id, params Expression<Func<Rental, object>>[] includeProperties);
    Task<Rental> DeleteRentalByIdAsync(int id);
}