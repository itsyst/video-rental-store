using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using Video.Application.Interfaces;
using Video.Domain.Entities;
using Video.Domain.Utilities;
using Video.Web.ViewModels;

namespace Video.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMovieRepository _movie;
    private IMemoryCache _cache;

    public HomeController(ILogger<HomeController> logger, IMovieRepository movie, IMemoryCache memoryCache)
    {
        _logger = logger;
        _movie = movie;
        _cache = memoryCache;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Movie> movies = await _movie.GetAllMoviesAsync(includeProperties: m => m.Genre);

        var cacheKey = "movies";

        // Set cache options.
        var memoryCacheEntryOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(Caching.Seconds)

        };

        _cache.Set(cacheKey, movies, memoryCacheEntryOptions);
        var cachedMovies = _cache.Get("movies");

        return View(cachedMovies);
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
