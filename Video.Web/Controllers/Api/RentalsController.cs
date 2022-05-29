using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Video.Application.Interfaces;
using Video.Application.Profiles.Dtos;
using Video.Domain.Entities;

namespace Video.Web.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class RentalsController : ControllerBase
{
    private readonly IRentalRepository _rental;
    private readonly ICustomerRepository _customer;
    private readonly IMovieRepository _movie;
    public RentalsController(
        ICustomerRepository customer,
        IMovieRepository movie,
        IRentalRepository rental
        )
    {
        _rental = rental;
        _customer = customer;
        _movie = movie;
    }

    // GET: api/<MoviesController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var rentals = await _rental.GetAllRentalsAsync(filter: null, r => r.Customer, r => r.Movie);
        return Ok(rentals);
    }

    // POST api/<RentalsController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RentalDto rentalDto)
    {
        var customerInDb = await _customer.GetCustomerByIdAsync(rentalDto.CustomerId);
        var moviesInDb = await _movie.GetAllMoviesAsync(filter: m => rentalDto.MovieIds.Contains(m.Id), m=>m.Genre);

        var rentals = await _rental.GetAllRentalsAsync(filter:r=>r.Customer.Id == rentalDto.CustomerId, r=>r.Movie, r=>r.Customer);


 
        foreach (var movie in moviesInDb.ToList())
        {
            if (movie.InStock == 0)
                return BadRequest("Movie is not available.");


            foreach (var item in rentals.ToList())
            {
                if (movie.Name == item.Movie.Name)
                {
                    return BadRequest("You already rented this movie.");
                }
            }

            movie.InStock--;

            await _movie.UpdateAsync(movie);
            await _customer.UpdateAsync(customerInDb);

            var rental = new Rental
            {
                Customer = customerInDb,
                Movie = movie,
                DateRented = DateTime.Now
            };

            await _rental.AddAsync(rental);
        }
        return Ok(rentalDto);
    }

    // DELETE api/rentals/5
    [HttpDelete("{id}")]
     public async Task<IActionResult> Delete(int id)
    {
        Rental rentalInDb = await _rental.GetRentalByIdAsync(id, r => r.Customer, r => r.Movie);

        if (rentalInDb == null)
            return NotFound();

        if (rentalInDb.DateReturned == null)
             return BadRequest();

        // We have to update the stock.
        rentalInDb.Movie.InStock++;
        await _movie.UpdateAsync(rentalInDb.Movie);

        await _rental.DeleteRentalByIdAsync(rentalInDb.Id);

        return NoContent();
    }
}
