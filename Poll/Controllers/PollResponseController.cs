using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
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
        private readonly IUserService _userService;

        public PollResponseController(ILogger<PollResponseController> logger, Main dbConn, IPollResponseService pollResponseService, IUserService userService)
        {
            _logger = logger;
            _dbConn = dbConn;
            _pollResponseService = pollResponseService;
            _userService = userService;
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
        public async Task<IActionResult> Save([FromBody] PollResponseViewModel pollRespose)
        {
            if (ModelState.IsValid)
            {
                if (App.CheckPwd.CheckValidPwd(pollRespose.user, _userService))
                {
                    pollRespose.respose.AnsweredId = pollRespose.user.Id;
                    pollRespose.respose.PollId = pollRespose.poll.Id;
                    var result = await _pollResponseService.AddOrUpdate(pollRespose.respose);
                    if (result > 0)
                        return Ok(result);

                    return BadRequest(result);
                }

                return Unauthorized();
            }

            return BadRequest();
        }
    }
}
