using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Video.Application.Interfaces;
using Video.Application.Utilities.Enums;
using Video.Domain;
using Video.Web.ViewModels;

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
        IEnumerable<MembershipTypes> membershipTypes = Enumeration.GetAll<MembershipTypes>();

        CustomerViewModel model = new()
        {
            Customer = new(),
            MembershipTypes = membershipTypes.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i .Value.ToString()
            })
        };

        if (id == Guid.Empty)
        {
            return View(model);
        }
        else
        {
            // update customer
            model.Customer = await _customer.GetByIdAsync(id);
            foreach (var type in membershipTypes)
            {
                if (type.Value.Equals(model.Customer.MembershipTypeId))
                    model.Customer.MembershipTypeId = (byte)type.Value;
            }
            return View(model);
        }
    }

}
