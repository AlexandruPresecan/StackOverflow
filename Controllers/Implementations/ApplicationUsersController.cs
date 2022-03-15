using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class ApplicationUsersController : ApiController<string, ApplicationUserDTO>
    {
        public ApplicationUsersController(ApplicationUsersService service) : base(service)
        {
        }
    }
}
