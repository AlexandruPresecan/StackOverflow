using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class QuestionVotesController : ApiController<int, QuestionVoteDTO>
    {
        public QuestionVotesController(QuestionVotesService service) : base(service)
        {
        }
    }
}
