using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class AnswersController : ApiController<int, AnswerDTO>
    {
        public AnswersController(ApplicationDbContext db)
        {
            _service = new AnswersService(db);
        }
    }
}
