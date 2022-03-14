using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class QuestionTagsController : ApiController<int, QuestionTagDTO>
    {
        public QuestionTagsController(ApplicationDbContext db)
        {
            _service = new QuestionTagsService(db);
        }
    }
}
