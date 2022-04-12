using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poll.Interfaces;
using Poll.Services;

namespace Poll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;
        private Main _dbConn;
        private readonly IQuestionService _questionService;

        public QuestionController(ILogger<QuestionController> logger, Main dbConn, IQuestionService questionService)
        {
            _logger = logger;
            _dbConn = dbConn;
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lst = await _questionService.GetAllAsync();
            if (lst.Any())
                return Ok(lst);

            return BadRequest("Nada foi encontrado.");
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Models.Question question)
        {
            if (ModelState.IsValid)
            {
                var result = await _questionService.AddOrUpdate(question);
                if (result > 0)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }
    }
}
