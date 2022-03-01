using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionVotesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public QuestionVotesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/<QuestionVotesController>
        [HttpGet]
        public IEnumerable<QuestionVote> Get()
        {
            return _db.QuestionVotes;
        }

        // GET api/<QuestionVotesController>/5
        [HttpGet("{id}")]
        public QuestionVote Get(int id)
        {
            return _db.QuestionVotes.Find(id);
        }

        // POST api/<QuestionVotesController>
        [HttpPost]
        public void Post([FromBody] QuestionVote vote)
        {
            if (!ModelState.IsValid)
                return;

            if (_db.QuestionVotes.Find(vote.Id) != null)
                return;

            if (_db.Users.Find(vote.AuthorId) == null)
                return;

            if (_db.Questions.Find(vote.QuestionId) == null)
                return;

            _db.QuestionVotes.Add(vote);
            _db.SaveChanges();
        }

        // PUT api/<QuestionVotesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] QuestionVote vote)
        {
            QuestionVote updatedVote = _db.QuestionVotes.Find(id);

            if (updatedVote == null)
                return;

            updatedVote.Value = vote.Value;
            
            _db.SaveChanges();
        }

        // DELETE api/<QuestionVotesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            QuestionVote vote = _db.QuestionVotes.Find(id);

            if (vote == null)
                return;

            _db.QuestionVotes.Remove(vote);
            _db.SaveChanges();
        }
    }
}
