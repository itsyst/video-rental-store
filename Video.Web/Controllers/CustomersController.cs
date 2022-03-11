using Microsoft.AspNetCore.Mvc;
using Video.Application.Interfaces;
using Video.Domain;

#nullable disable

namespace Video.Web.Controllers;

public class CustomersController : Controller
{
    private readonly ICustomerRepository _customer;
    public CustomersController(ICustomerRepository customer)
    {
        _customer = customer;
    }
    public async Task<ActionResult> Index()
    {
        IEnumerable<Customer> customers = await _customer.GetAllCustomersAsync(includeProperties: c => c.MembershipType);
        return View(customers);
    }
    public async Task<IActionResult> Upsert(Guid id)
    {
        Customer customer = new();

        if (id == Guid.Empty)
        {
            return View(customer);
        }
        else
        {
            // update customer
            customer = await _customer.GetByIdAsync(id);
            return View(customer);
        }
    }

}
