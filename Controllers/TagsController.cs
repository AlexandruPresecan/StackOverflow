using Microsoft.AspNetCore.Mvc;
using StackOverflow.Data;
using StackOverflow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public TagsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/<TagsController>
        [HttpGet]
        public IEnumerable<Tag> Get()
        {
            return _db.Tags;
        }

        // GET api/<TagsController>/5
        [HttpGet("{id}")]
        public Tag Get(int id)
        {
            return _db.Tags.Find(id);
        }

        // POST api/<TagsController>
        [HttpPost]
        public void Post([FromBody] Tag tag)
        {
            if (!ModelState.IsValid)
                return;

            if (_db.Tags.Find(tag.Id) != null)
                return;

            _db.Tags.Add(tag);
            _db.SaveChanges();
        }

        // PUT api/<TagsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Tag tag)
        {
            Tag updatedTag = _db.Tags.Find(id);

            if (updatedTag == null)
                return;

            updatedTag.Name = tag.Name == null ? updatedTag.Name : tag.Name;
            
            _db.SaveChanges();
        }

        // DELETE api/<TagsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Tag tag = _db.Tags.Find(id);

            if (tag == null)
                return;

            _db.Tags.Remove(tag);
            _db.SaveChanges();
        }
    }
}
