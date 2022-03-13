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

    // GET: Customers/Upsert/b86104b6-7205-4d5a-ab83-0eb534c0ae60
    public async Task<IActionResult> Upsert(Guid id)
    {
        IEnumerable<MembershipTypes> membershipTypes = Enumeration.GetAll<MembershipTypes>();

        CustomerViewModel model = new()
        {
            Customer = new(),
            MembershipTypes = membershipTypes.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Value.ToString()
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

    // GET: Customers/Upsert/b86104b6-7205-4d5a-ab83-0eb534c0ae60
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(CustomerViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Customer.Id.Equals(Guid.Empty))
            {
                await _customer.AddAsync(model.Customer);
                TempData["Success"] = "Customer created successfully.";
            }
            else
            {
                await _customer.UpdateAsync(model.Customer);
                TempData["Success"] = "Customer updated successfully.";
            }

            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}
