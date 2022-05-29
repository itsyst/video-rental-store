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
    private readonly IMovieRepository _movie;
    public RentalsController(
        IRentalRepository rental,
        IMovieRepository movie)
    {
        _rental = rental;
        _movie = movie;
    }

    public async Task<ActionResult> Index()
    {
        IEnumerable<Rental> rentals = await _rental.GetAllRentalsAsync(filter:null, r=>r.Customer, r=>r.Movie);
        return View(rentals);
    }

    // GET: Customers 
    public async Task<IActionResult> Create()
    {
        return await Task.FromResult(View());
    }


    public async Task<IActionResult> Return(int id)
    {
        if (id == 0)
            return NotFound();

        var rentalInDb = await _rental.GetByIdAsync(id);
 
        var rentals = await _rental.GetAllRentalsAsync(r=>r.Id == id, r => r.Customer, r => r.Movie);

        foreach (var rental in rentals.ToList())
        {
            rental.Movie.InStock++;
            await _movie.UpdateAsync(rental.Movie);
            rental.DateReturned = DateTime.Now;

            await _rental.UpdateAsync(rental);
        }

        return RedirectToAction(nameof(Index));
    }

}
