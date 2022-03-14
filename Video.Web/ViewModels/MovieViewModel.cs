using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Video.Domain;

namespace Video.Web.ViewModels;

public class MovieViewModel
{
    [ValidateNever]
    public Movie? Movie { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem>? Genres { get; set; }
}