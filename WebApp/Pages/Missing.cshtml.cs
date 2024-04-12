using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class MissingModel : PageModel
    {
        public IActionResult OnGet()
        {
            return NotFound();
        }
    }
}
