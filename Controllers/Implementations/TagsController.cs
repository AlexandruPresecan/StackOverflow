using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class TagsController : ApiController<int, string>
    {
        public TagsController(TagsService service) : base(service)
        {
        }
    }
}
