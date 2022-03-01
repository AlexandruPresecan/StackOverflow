using Microsoft.AspNetCore.Mvc;
using StackOverflow.Data;
using StackOverflow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerVotesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public AnswerVotesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/<AnswerVotesController>
        [HttpGet]
        public IEnumerable<AnswerVote> Get()
        {
            return _db.AnswerVotes;
        }

        // GET api/<AnswerVotesController>/5
        [HttpGet("{id}")]
        public AnswerVote Get(int id)
        {
            return _db.AnswerVotes.Find(id);
        }

        // POST api/<AnswerVotesController>
        [HttpPost]
        public void Post([FromBody] AnswerVote vote)
        {
            if (!ModelState.IsValid)
                return;

            if (_db.AnswerVotes.Find(vote.Id) != null)
                return;

            if (_db.AnswerVotes.Find(vote.AuthorId) == null)
                return;

            if (_db.AnswerVotes.Find(vote.AnswerId) == null)
                return;

            _db.AnswerVotes.Add(vote);
            _db.SaveChanges();
        }

        // PUT api/<AnswerVotesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AnswerVote vote)
        {
            AnswerVote updatedVote = _db.AnswerVotes.Find(id);

            if (updatedVote == null)
                return;

            updatedVote.Value = vote.Value;
            
            _db.SaveChanges();
        }

        // DELETE api/<AnswerVotesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            AnswerVote vote = _db.AnswerVotes.Find(id);

            if (vote == null)
                return;

            _db.AnswerVotes.Remove(vote);
            _db.SaveChanges();
        }
    }
}
