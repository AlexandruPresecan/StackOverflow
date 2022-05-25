using Microsoft.AspNetCore.Mvc;
using StackOverflow.Services;

namespace StackOverflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController<K, V> : ControllerBase
    {
        protected readonly IService<K, V> _service;

        public ApiController(IService<K, V> service)
        {
            _service = service;
        }
        
        public IActionResult ServiceResultToAction(ServiceResult result)
        {
            return result.Success ? Ok(result.Value) : BadRequest(result.Value);
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            return ServiceResultToAction(_service.Get());
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(K id)
        {
            return ServiceResultToAction(_service.Get(id));
        }

        [HttpPost]
        [Authorize]
        public virtual IActionResult Post([FromBody] V value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(m => m.Errors));

            return ServiceResultToAction(_service.Post(value, HttpContext));
        }

        [HttpPut("{id}")]
        [Authorize]
        public virtual IActionResult Put(K id, [FromBody] V value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(m => m.Errors));

            return ServiceResultToAction(_service.Put(id, value, HttpContext));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public virtual IActionResult Delete(K id)
        {
            return ServiceResultToAction(_service.Delete(id, HttpContext));
        }
    }
}
