using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Video.Application.Interfaces;
using Video.Application.Profiles.Dtos;
using Video.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Video.Web.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _movie;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IMapper _mapper;

    public MoviesController(
        IMovieRepository movie,
        IWebHostEnvironment hostEnvironment,
        IMapper mapper)
    {
        _movie = movie;
        _hostEnvironment = hostEnvironment;
        _mapper = mapper;
    }
    // GET: api/<MoviesController>
    [HttpGet]
    public async Task<IActionResult> Get(string query = null)
    {
        var movies = await _movie.GetAllMoviesAsync(filter: m => m.InStock >0 && m.Name.Contains(query), includeProperties: m => m.Genre);

        return Ok(movies.Select(_mapper.Map<Movie, MovieDto>));
    }

    // GET api/<MoviesController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var movieIndB = await _movie.GetByIdAsync(id);
        if (movieIndB == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<Movie, MovieDto>(movieIndB));
    }

    // POST api/<MoviesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MovieDto movieDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var _mappedMovie = _mapper.Map<MovieDto, Movie>(movieDto);
        movieDto.Id = _mappedMovie.Id;
        await _movie.AddAsync(_mappedMovie);

        return CreatedAtAction(nameof(Get), new { id = _mappedMovie.Id }, _mappedMovie);
    }

    // PUT api/<MoviesController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] MovieDto movieDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var movieIndB = await _movie.GetByIdAsync(id);
        if (movieIndB == null)
        {
            return NotFound();
        }

        var _mappedMovie = _mapper.Map(movieDto, movieIndB);
        movieIndB.Id = _mappedMovie.Id;

        await _movie.UpdateAsync(_mappedMovie);

        return Ok(_mappedMovie);

    }

    // DELETE api/<MoviesController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movieIndB = await _movie.GetByIdAsync(id);

        if (movieIndB == null)
        {
            return NotFound();
        }

        if (movieIndB.ImageUrl != null)
        {
            string oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, movieIndB.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
        }

        await _movie.DeleteAsync(id);

        return NoContent();
    }
}
