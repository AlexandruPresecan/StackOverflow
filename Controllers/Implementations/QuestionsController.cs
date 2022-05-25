using Microsoft.AspNetCore.Mvc;
using StackOverflow.DTOs;
using StackOverflow.Services;

namespace StackOverflow.Controllers.Implementations
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController: ApiController<int, QuestionDTO>
    {
        public QuestionsController(QuestionsService service) : base(service)
        {
        }

        [HttpGet("tagged/{tag}")]
        public IActionResult GetByTag(string tag)
        {
            return ServiceResultToAction(((QuestionsService)_service).GetByTag(tag));
        }

        [HttpGet("search/{name}")]
        public IActionResult GetByName(string name)
        {
            return ServiceResultToAction(((QuestionsService)_service).GetByName(name));
        }
    }
}
