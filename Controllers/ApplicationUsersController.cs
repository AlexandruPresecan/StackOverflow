using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/<ApplicationUsersController>
        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            return _userManager.Users;
        }

        // GET api/<ApplicationUsersController>/5
        [HttpGet("{id}")]
        public ApplicationUser Get(string id)
        {
            return _userManager.FindByIdAsync(id).Result;
        }

        // POST api/<ApplicationUsersController>
        [HttpPost]
        public void Post([FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
                return;

            _userManager.CreateAsync(user);
        }

        // PUT api/<ApplicationUsersController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ApplicationUser user)
        {
            ApplicationUser updatedUser = _userManager.FindByIdAsync(id).Result;

            if (updatedUser == null)
                return;

            if (user.PhoneNumber != null)
                _userManager.SetPhoneNumberAsync(updatedUser, user.PhoneNumber);

            if (user.Email != null)
            {
                _userManager.SetEmailAsync(updatedUser, user.Email);
                _userManager.UpdateNormalizedEmailAsync(updatedUser);
            }

            if (user.UserName != null)
            {
                _userManager.SetUserNameAsync(updatedUser, user.UserName);
                _userManager.UpdateNormalizedUserNameAsync(updatedUser);
            }

            _userManager.UpdateAsync(updatedUser);
        }

        // DELETE api/<ApplicationUsersController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            ApplicationUser user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
                return;

            _userManager.DeleteAsync(user);
        }
    }
}
