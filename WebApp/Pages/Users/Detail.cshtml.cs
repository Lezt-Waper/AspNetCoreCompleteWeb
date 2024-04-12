using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages.Users
{
    public class DetailModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public DetailModel(IUserRepository repository)
        {
            _userRepository = repository;
        }

        [BindProperty]
        public User User { get; set; }

        public async void OnGet(int Id)
        {
            try
            {
                var result = await _userRepository.Get(Id);

                if (result == null)
                {
                    throw new Exception("User is not exists");
                }

                User = result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var result = await _userRepository.Update(User);

                if (result == 0)
                {
                    throw new Exception("Error when updating data");
                }
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToPage("/Users/Index");
        }
    }
}
