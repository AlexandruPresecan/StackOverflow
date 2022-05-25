using Microsoft.AspNetCore.Mvc;
using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class ApplicationUsersController : ApiController<string, ApplicationUserDTO>
    {
        public ApplicationUsersController(ApplicationUsersService service) : base(service)
        {
        }

        [HttpPost("authentication/login")]
        public IActionResult Login([FromBody] ApplicationUserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(m => m.Errors));

            return ServiceResultToAction(((ApplicationUsersService)_service).Authenticate(user));
        }

        [HttpPost("authentication/register")]
        public IActionResult Register([FromBody] ApplicationUserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(m => m.Errors));

            return ServiceResultToAction(_service.Post(user, HttpContext));
        }

        [HttpPut("ban/{id}")]
        [Authorize]
        public IActionResult Ban(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(m => m.Errors));

            return ServiceResultToAction(((ApplicationUsersService)_service).Ban(id, true, HttpContext));
        }

        [HttpPut("unban/{id}")]
        [Authorize]
        public IActionResult UnBan(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(m => m.Errors));

            return ServiceResultToAction(((ApplicationUsersService)_service).Ban(id, false, HttpContext));
        }
    }
}
