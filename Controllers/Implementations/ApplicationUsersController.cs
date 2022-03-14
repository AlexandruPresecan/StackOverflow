using Microsoft.AspNetCore.Identity;
using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Models;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class ApplicationUsersController : ApiController<string, ApplicationUserDTO>
    {
        public ApplicationUsersController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _service = new ApplicationUsersService(db, userManager);
        }
    }
}
