using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers.Implementations
{
    public class QuestionsController : ApiController<int, QuestionDTO>
    {
        public QuestionsController(QuestionsService service) : base(service)
        {
        }
    }
}
