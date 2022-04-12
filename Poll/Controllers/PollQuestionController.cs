using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poll.Interfaces;
using Poll.Services;

namespace Poll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollQuestionController : ControllerBase
    {
        private readonly ILogger<PollQuestionController> _logger;
        private Main _dbConn;
        private readonly IPollQuestionService _pollQuestionService;

        public PollQuestionController(ILogger<PollQuestionController> logger, Main dbConn, IPollQuestionService pollQuestionService)
        {
            _logger = logger;
            _dbConn = dbConn;
            _pollQuestionService = pollQuestionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllFromPoll(int id)
        {
            var item = await _pollQuestionService.GetAllFromPollAsync(id);
            if (item == null)
                return BadRequest("Nada foi encontrado.");

            return Ok(item);
        }


        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Models.PollQuestion pollQuestion)
        {
            if (ModelState.IsValid)
            {
                var result = await _pollQuestionService.AddOrUpdate(pollQuestion);
                if (result > 0)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }
    }
}
