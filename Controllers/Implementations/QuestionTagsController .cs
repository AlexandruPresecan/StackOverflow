using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class QuestionTagsController : ApiController<int, QuestionTagDTO>
    {
        public QuestionTagsController(QuestionTagsService service) : base(service)
        {
        }
    }
}
