using Microsoft.AspNetCore.Mvc;
using StackOverflow.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController<K, V> : ControllerBase
    {
        private readonly IService<K, V> _service;

        public ApiController(IService<K, V> service)
        {
            _service = service;
        }

        public IActionResult ServiceResultToAction(ServiceResult result)
        {
            return result.Success ? Ok(result.Value) : BadRequest(result.Value);
        }

        // GET: api/<ApiController>
        [HttpGet]
        public IActionResult Get()
        {
            return ServiceResultToAction(_service.Get());
        }

        // GET api/<ApiController>/5
        [HttpGet("{id}")]
        public IActionResult Get(K id)
        {
            return ServiceResultToAction(_service.Get(id));
        }

        // POST api/<ApiController>
        [HttpPost]
        public IActionResult Post([FromBody] V value)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Field");

            return ServiceResultToAction(_service.Post(value));
        }

        // PUT api/<ApiController>/5
        [HttpPut("{id}")]
        public IActionResult Put(K id, [FromBody] V value)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Field");

            return ServiceResultToAction(_service.Put(id, value));
        }

        // DELETE api/<ApiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(K id)
        {
            return ServiceResultToAction(_service.Delete(id));
        }
    }
}
