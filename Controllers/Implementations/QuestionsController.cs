using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers.Implementations
{
    public class QuestionsController : ApiController<int, QuestionDTO>
    {
        public QuestionsController(ApplicationDbContext db)
        {
            _service = new QuestionsService(db);
        }
    }
}
