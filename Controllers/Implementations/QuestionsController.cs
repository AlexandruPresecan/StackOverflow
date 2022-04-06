using Microsoft.AspNetCore.Mvc;
using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers.Implementations
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController: ControllerBase
    {
        private readonly QuestionsService _service;

        public QuestionsController(QuestionsService service)
        {
            _service = service;
        }

        public IActionResult ServiceResultToAction(ServiceResult result)
        {
            return result.Success ? Ok(result.Value) : BadRequest(result.Value);
        }

        // GET: api/<ApiController>
        [HttpGet]
        public IActionResult Get([FromQuery(Name = "tag")] string? tag = null)
        {
            if (tag == null)
                return ServiceResultToAction(_service.Get());

            return ServiceResultToAction(_service.Get(tag));
        }

        // GET api/<ApiController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return ServiceResultToAction(_service.Get(id));
        }

        // POST api/<ApiController>
        [HttpPost]
        public IActionResult Post([FromBody]QuestionDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Field");

            return ServiceResultToAction(_service.Post(value));
        }

        // PUT api/<ApiController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] QuestionDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Field");

            return ServiceResultToAction(_service.Put(id, value));
        }

        // DELETE api/<ApiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return ServiceResultToAction(_service.Delete(id));
        }
    }
}
