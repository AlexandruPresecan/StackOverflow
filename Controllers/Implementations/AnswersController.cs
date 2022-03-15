using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class AnswersController : ApiController<int, AnswerDTO>
    {
        public AnswersController(AnswersService service) : base(service)
        {
        }
    }
}
