using Video.Application.Interfaces;
using Video.Domain;
using Video.Infrastructure.Persistence;

namespace Video.Infrastructure.Services;

public class CustomerService : BaseService<Customer>, ICustomerRepository
{
    public CustomerService(ApplicationDbContext context) : base(context)
    {
    }
}
