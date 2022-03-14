using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    public class QuestionVotesController : ApiController<int, QuestionVoteDTO>
    {
        public QuestionVotesController(ApplicationDbContext db)
        {
            _service = new QuestionVotesService(db);
        }
    }
}
