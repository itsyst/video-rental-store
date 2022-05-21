using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Video.Application.Interfaces;
using Video.Domain.Entities;
using Video.Domain.Utilities;

#nullable disable

namespace Video.Web.Controllers;

[Authorize(Roles = Roles.Admin)]
public class RentalsController : Controller
{
    private readonly IRentalRepository _rental;
    public RentalsController(IRentalRepository rental)
    {
        _rental = rental;
    }

    // GET: Customers/Upsert/5
    public async Task<IActionResult> Create()
    {
        return await Task.FromResult(View());
    }
}
