using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public QuestionsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/<QuestionsController>
        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return _db.Questions.OrderBy(q => q.CreationDate);
        }

        // GET api/<QuestionsController>/5
        [HttpGet("{id}")]
        public Question Get(int id)
        {
            return _db.Questions.Find(id);
        }

        // POST api/<QuestionsController>
        [HttpPost]
        public void Post([FromBody] Question question)
        {
            if (!ModelState.IsValid)
                return;

            if (_db.Questions.Find(question.Id) != null)
                return;

            if (_db.Users.Find(question.AuthorId) == null)
                return;

            question.CreationDate = DateTime.Now;

            _db.Questions.Add(question);
            _db.SaveChanges();
        }

        // PUT api/<QuestionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Question question)
        {
            Question updatedQuestion = _db.Questions.Find(id);

            if (updatedQuestion == null)
                return;

            updatedQuestion.Text = question.Text == null ? updatedQuestion.Text : question.Text;
            updatedQuestion.Title = question.Title == null ? updatedQuestion.Title : question.Title;
            
            _db.SaveChanges();
        }

        // DELETE api/<QuestionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Question question = _db.Questions.Find(id);

            if (question == null)
                return;

            _db.Questions.Remove(question);
            _db.SaveChanges();
        }
    }
}
