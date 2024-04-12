using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using WebApp.Models;

namespace WebApp.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserRepository repository;

        public CreateModel(IUserRepository repository)
        {
            this.repository = repository;
        }

        [BindProperty]
        public User User { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (User.FirstName == User.LastName)
            {
                ModelState.AddModelError(string.Empty, 
                    "First Name and Last Name can't be same");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await repository.Create(User);
            return RedirectToPage("/Users/Index");
        }
    }
}
