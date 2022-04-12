using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poll.Interfaces;

namespace Poll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollResponseController : ControllerBase
    {
        private readonly ILogger<PollResponseController> _logger;
        private Main _dbConn;
        private readonly IPollResponseService _pollResponseService;

        public PollResponseController(ILogger<PollResponseController> logger, Main dbConn, IPollResponseService pollResponseService)
        {
            _logger = logger;
            _dbConn = dbConn;
            _pollResponseService = pollResponseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lst = await _pollResponseService.GetAllWithoutUserAsync();
            if (lst.Any())
                return Ok(lst);

            return BadRequest("Nada foi encontrado.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllFromPoll(int id)
        {
            var item = await _pollResponseService.GetAllFromPollAsync(id);
            if (item == null)
                return BadRequest("Nada foi encontrado.");

            return Ok(item);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllFromUser(int id)
        {
            var item = await _pollResponseService.GetAllFromUserAsync(id);
            if (item == null)
                return BadRequest("Nada foi encontrado.");

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Models.PollRespose pollRespose)
        {
            if (ModelState.IsValid)
            {
                var result = await _pollResponseService.AddOrUpdate(pollRespose);
                if (result > 0)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }
    }
}
