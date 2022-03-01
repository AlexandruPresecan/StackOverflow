using Microsoft.AspNetCore.Mvc;
using StackOverflow.Data;
using StackOverflow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public AnswersController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/<AswersController>
        [HttpGet]
        public IEnumerable<Answer> Get()
        {
            return _db.Answers.OrderBy(a => a.VoteCount);
        }

        // GET api/<AswersController>/5
        [HttpGet("{id}")]
        public Answer Get(int id)
        {
            return _db.Answers.Find(id);
        }

        // POST api/<AswersController>
        [HttpPost]
        public void Post([FromBody] Answer answer)
        {
            if (!ModelState.IsValid)
                return;

            if (_db.Answers.Find(answer.Id) != null)
                return;

            if (_db.Users.Find(answer.AuthorId) == null)
                return;

            if (_db.Questions.Find(answer.QuestionId) == null)
                return;

            answer.CreationDate = DateTime.Now;

            _db.Answers.Add(answer);
            _db.SaveChanges();
        }

        // PUT api/<AswersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Answer answer)
        {
            Answer updatedAnswer = _db.Answers.Find(id);

            if (updatedAnswer == null)
                return;

            updatedAnswer.Text = answer.Text == null ? updatedAnswer.Text : answer.Text;
            
            _db.SaveChanges();
        }

        // DELETE api/<AswersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Answer answer = _db.Answers.Find(id);

            if (answer == null)
                return;

            _db.Answers.Remove(answer);
            _db.SaveChanges();
        }
    }
}
