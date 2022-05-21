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
    private readonly IMapper _mapper;
    public RentalsController(
        IRentalRepository rental,
         IMapper mapper
        )
    {
        _rental = rental;
        _mapper = mapper;   
    }

    // GET: api/<RentalsController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var rentals = await _rental.GetAllRentalsAsync(r => r.Movie, r=>r.Customer);

        return Ok(rentals.Select(_mapper.Map<Rental, RentalDto>));


    }

    // GET api/customers/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var rentalInDb = await _rental.GetRentalByIdAsync(id);
        if (rentalInDb == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<Rental, RentalDto>(rentalInDb));
    }

    // POST api/<RentalsController>
    [HttpPost]
    public async Task<IActionResult> Post(RentalDto rentalDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var customer = await _customer.GetCustomerByIdAsync(rentalDto.Customer!.Id);

        var movies = await _movie.GetAllMoviesAsync(m=>m.Id == rentalDto.Movie.Id);

        foreach (var movie in movies)
        {
            if (movie.InStock == 0)
                return BadRequest("Movie is not available.");

            movie.InStock--;

            var rental = new Rental
            {
                Customer = customer,
                Movie = movie,
                DateRented = DateTime.Now
            };

            await _rental.AddAsync(rental);
        }
        return Ok();
    }
}
