using Microsoft.AspNetCore.Mvc;
using Video.Application.Interfaces;
using Video.Domain;

namespace Video.Web.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieRepository _movie;

    public MoviesController(IMovieRepository movie)
    {
        _movie = movie;
    }
    public async Task<IActionResult> Index()
    {
        IEnumerable<Movie> movies = await _movie.GetAllMoviesAsync(includeProperties: m => m.Genre);
        return View(movies);
    }
}
