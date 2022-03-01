using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTagsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public QuestionTagsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/<QuestionTagsController>
        [HttpGet]
        public IEnumerable<QuestionTag> Get()
        {
            return _db.QuestionTags.Include(q => q.Question).Include(q => q.Tag);
        }

        // GET api/<QuestionTagsController>/5
        [HttpGet("{id}")]
        public QuestionTag Get(int id)
        {
            return _db.QuestionTags.Find(id);
        }

        // POST api/<QuestionTagsController>
        [HttpPost]
        public void Post([FromBody] QuestionTag questionTag)
        {
            if (!ModelState.IsValid)
                return;

            if (_db.QuestionTags.Find(questionTag.Id) != null)
                return;

            if (_db.Questions.Find(questionTag.QuestionId) != null)
                return;

            if (_db.Tags.Find(questionTag.TagId) != null)
                return;

            _db.QuestionTags.Add(questionTag);
            _db.SaveChanges();
        }

        // PUT api/<QuestionTagsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] QuestionTag questionTag)
        {

        }

        // DELETE api/<QuestionTagsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            QuestionTag questionTag = _db.QuestionTags.Find(id);

            if (questionTag == null)
                return;

            _db.QuestionTags.Remove(questionTag);
            _db.SaveChanges();
        }
    }
}
