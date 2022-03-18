using Microsoft.AspNetCore.Mvc;
using Video.Application.Interfaces;
using Video.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Video.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieRepository _movie;
        private IWebHostEnvironment _hostEnvironment;

        public MoviesController(
            IMovieRepository movie,
            IWebHostEnvironment hostEnvironment)
        {
            _movie = movie;
            _hostEnvironment = hostEnvironment;
        }
        // GET: api/<MoviesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movies = await _movie.GetAllMoviesAsync(includeProperties: m => m.Genre);

            return Ok(movies);
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var movieIndB = await _movie.GetByIdAsync(id);
            if (movieIndB == null)
                return NotFound();

            return Ok(movieIndB);
        }

        // POST api/<MoviesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _movie.AddAsync(movie);

            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieIndB = await _movie.GetByIdAsync(id);
            if (movieIndB == null)
                return NotFound();

            movieIndB.Name = movie.Name;
            movieIndB.ImageUrl = movie.ImageUrl;
            movieIndB.ReleaseDate = movie.ReleaseDate;
            movieIndB.CreatedDate = movie.CreatedDate;
            movieIndB.InStock = movie.InStock;
            movieIndB.GenreId = movie.GenreId;

            await _movie.UpdateAsync(movieIndB);

            return Ok(movieIndB);

        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movieIndB = await _movie.GetByIdAsync(id);

            if (movieIndB == null)
                return NotFound();

            if (movieIndB.ImageUrl != null)
            {
                string oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, movieIndB.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath)) System.IO.File.Delete(oldImagePath);
            }

            await _movie.DeleteAsync(id);

            return NoContent();
        }
    }
}
