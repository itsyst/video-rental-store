using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Video.Application.Interfaces;
using Video.Domain.Entities;
using Video.Domain.Enums;
using Video.Web.ViewModels;

#nullable disable

namespace Video.Web.Controllers;

[Authorize]
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
            model.Customer = await _customer.GetCustomerByIdAsync(id);
            foreach (var type in membershipTypes)
            {
                if (type.Value.Equals(model.Customer.MembershipTypeId))
                {
                    model.Customer.MembershipTypeId = (byte)type.Value;
                }
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
            //check whether name is already exists in the database or not
            var customers = await _customer.GetAllAsync();

            foreach (var customer in customers)
            {
                if (customer.FirstName == model.Customer.FirstName
                    && customer.LastName == model.Customer.LastName
                    && customer.Birthdate == model.Customer.Birthdate)
                {
                    //adding error message to ModelState
                    ModelState.AddModelError("FirstName", "Customer Already Exists.");

                    return View(model);
                }
            }

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

    // GET: /Customers/Delete/b86104b6-7205-4d5a-ab83-0eb534c0ae60
    public async Task<IActionResult> Delete(Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        Customer customer = await _customer.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    // POST: /Customers/Delete/b86104b6-7205-4d5a-ab83-0eb534c0ae60
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        Customer customer = await _customer.GetCustomerByIdAsync(id);
        await _customer.DeleteCustomerByIdAsync(customer.Id);
        TempData["Success"] = "Customer deleted successfully.";

        return RedirectToAction("Index");
    }
}
