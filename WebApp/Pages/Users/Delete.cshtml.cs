using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public DeleteModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public User User { get; set; }

        public async void OnGet(int Id)
        {
            try
            {
                var user = await _userRepository.Get(Id);
                if (user == null)
                {
                    throw new Exception("User is not exists");
                }

                User = user;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var result = await _userRepository.Delete(User.Id);
                if (result == null)
                {
                    throw new Exception("Something wrongs happen");
                }

                return RedirectToPage("/Users/Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
