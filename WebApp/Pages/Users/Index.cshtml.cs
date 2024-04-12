using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserRepository repository;
        private readonly ILogger<IndexModel> logger;

        public List<User> Users { get; set; }
        public IndexModel(IUserRepository repository, ILogger<IndexModel> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        public async Task<IActionResult> OnGet(int id = 0)
        {
            if (id == 0)
            {
                try
                {
                    var result = await repository.Get();
                    Users = result.ToList();

                    using(logger.BeginScope(new Dictionary<string, object> { { "ScopeName", 123 } }))
                    {
                        logger.LogInformation("Load {userCount} User", Users.Count);
                    }

                    return Page();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            try
            {
                var result = await repository.Get(id);
                if (result == null)
                {
                    throw new Exception("User is not exists");
                }

                Users = new List<User> { result };

                return Page();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("Success");
        }
    }
}
