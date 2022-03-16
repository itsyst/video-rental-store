using Microsoft.AspNetCore.Mvc.Rendering;
using Video.Domain.Entities;

namespace Video.Web.ViewModels;

public class CustomerViewModel
{
    public Customer? Customer{ get; set; }

    public IEnumerable<SelectListItem>? MembershipTypes { get; set; }

}
