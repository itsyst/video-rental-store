using Video.Domain;

namespace Video.Web.ViewModels;

public class MovieViewModel
{
    public Movie? Movie { get; set; }
    public IEnumerable<Customer>? Customers { get; set; }
}
