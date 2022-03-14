using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class AnswerVotesController : ApiController<int, AnswerVoteDTO>
    {
        public AnswerVotesController(ApplicationDbContext db)
        {
            _service = new AnwserVotesService(db);
        }
    }
}
