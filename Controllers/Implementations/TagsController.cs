using StackOverflow.Data;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class TagsController : ApiController<int, string>
    {
        public TagsController(ApplicationDbContext db)
        {
            _service = new TagsService(db);
        }
    }
}
