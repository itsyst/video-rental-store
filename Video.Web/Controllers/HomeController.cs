using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Video.Application.Interfaces;
using Video.Domain.Entities;
using Video.Web.ViewModels;

namespace Video.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMovieRepository _movie;


    public HomeController(ILogger<HomeController> logger, IMovieRepository movie)
    {
        _logger = logger;
        _movie = movie;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Movie> movies = await _movie.GetAllMoviesAsync(includeProperties: m => m.Genre);
        return View(movies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
