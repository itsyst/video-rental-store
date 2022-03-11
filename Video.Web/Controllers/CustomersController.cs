using Microsoft.AspNetCore.Mvc;
using Video.Application.Interfaces;
using Video.Domain;

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
        IEnumerable<Customer> customers = await _customer.GetAllAsync();
        return View(customers);
    }
}
