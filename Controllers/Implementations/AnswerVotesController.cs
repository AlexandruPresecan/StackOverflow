using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class AnswerVotesController : ApiController<int, AnswerVoteDTO>
    {
        public AnswerVotesController(AnswerVotesService service) : base(service)
        {
        }
    }
}
