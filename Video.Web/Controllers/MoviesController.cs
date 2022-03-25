using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Video.Application.Interfaces;
using Video.Domain.Entities;
using Video.Web.ViewModels;

namespace Video.Web.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieRepository _movie;
    private readonly IGenreRepository _genre;
    private readonly IWebHostEnvironment _hostEnvironment;

    public MoviesController(
        IMovieRepository movie,
        IGenreRepository genre,
        IWebHostEnvironment hostEnvironment)
    {
        _movie = movie;
        _genre = genre;
        _hostEnvironment = hostEnvironment;
    }
    public async Task<IActionResult> Index()
    {
        return await Task.FromResult(View());
    }

    // GET: movies/Upsert/b86104b6-7205-4d5a-ab83-0eb534c0ae60
    public async Task<IActionResult> Upsert(int id)
    {
        IEnumerable<Genre> genres = await _genre.GetAllAsync();
        MovieViewModel model = new()
        {
            Movie = new(),
            Genres = genres.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            })
        };

        if (id == 0)
        {
            return View(model);
        }
        else
        {
            // update customer
            model.Movie = await _movie.GetByIdAsync(id);
            return View(model);
        }
    }

    // GET: Customers/Upsert/b86104b6-7205-4d5a-ab83-0eb534c0ae60
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(MovieViewModel model, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            if (file != null)
            {
                UploadPoster(model, file);
            }

            if (model.Movie!.Id == 0)
            {
                model.Movie.CreatedDate = DateTime.Now;
                await _movie.AddAsync(model.Movie);
                TempData["Success"] = "Movie created successfully.";
            }
            else
            {
                await _movie.UpdateAsync(model.Movie);
                TempData["Success"] = "Movie updated successfully.";
            }

            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public void ResizeImage(IFormFile file, string path)
    {
        using var image = Image.Load(file.OpenReadStream());
        image.Mutate(x => x.Resize(600, 900));
        image.Save(path);
    }

    private void UploadPoster(MovieViewModel model, IFormFile file)
    {
        string wwwRootPath = _hostEnvironment.WebRootPath;

        string fileName = Guid.NewGuid().ToString();
        var uploads = Path.Combine(wwwRootPath, @"uploads\posters");
        var extension = Path.GetExtension(file.FileName).ToLower();

        if (model.Movie!.ImageUrl != null)
        {
            var oldImagePath = Path.Combine(wwwRootPath, model.Movie.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
        }


        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
        {
            file.CopyTo(fileStreams);
        }

        model.Movie.ImageUrl = @"\uploads\posters\" + fileName + extension;


        ResizeImage(file, Path.Combine(uploads, fileName + extension));


    }
}
